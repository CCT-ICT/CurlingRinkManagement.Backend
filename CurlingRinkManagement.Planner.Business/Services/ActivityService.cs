using CurlingRinkManagement.Common.Business.Services;
using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Core.Data.Enums;
using CurlingRinkManagement.Planner.Data.DatabaseModels;
using CurlingRinkManagement.Planner.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurlingRinkManagement.Planner.Business.Services;

public class ActivityService(IClubRepository<Activity> _activityRepository, IClubRepository<SheetActivity> _sheetActivityRepository) : BaseService<Activity>(_activityRepository), IActivityService
{
    public override Activity Create(Activity activity)
    {
        var clubId = _entityRepository.GetClubId();

        foreach (var sheet in activity.SheetActivities)
        {
            sheet.ClubId = clubId;
            sheet.ActivityTime.ClubId = clubId;
        }

        return _entityRepository.Create(activity);
    }

    public override IQueryable<Activity> AddIncludes(IQueryable<Activity> query)
    {
        return query.Include(a => a.SheetActivities).ThenInclude(s => s.ActivityTime).Include(a => a.SheetActivities).ThenInclude(s => s.LinkedInstructors);
    }

    public override IQueryable<Activity> AddFilter(IQueryable<Activity> query, string[] filters, string[] filterValues)
    {
        if (filters.Length != filterValues.Length) return query;
        for (int i = 0; i < filters.Length; i++)
        {
            var filter = filters[i];
            var filterValue = filterValues[i].ToLower().Replace(" ", "");
            if (!Enum.TryParse<GenericFilters>(filter, out var parsedFilter))
                continue;
            switch (parsedFilter)
            {
                case GenericFilters.SheetId:
                    if (!Guid.TryParse(filterValue, out var sheetId))
                        break;

                    query = query.Where(a => a.SheetActivities.Any(s => s.SheetId == sheetId));
                    break;
                case GenericFilters.InstructorId:
                    query = query.Where(a => a.SheetActivities.Any(s => s.LinkedInstructors.Any(l => l.UserIdentity.Equals(filterValue))));
                    break;
                case GenericFilters.StartDate:
                    if (!DateTime.TryParse(filterValue, out var startTime))
                        break;
                    startTime = startTime.ToUniversalTime();
                    query = query.Where(a => a.SheetActivities.Any(s => s.ActivityTime.Start >= startTime || s.ActivityTime.End >= startTime));
                    break;
                case GenericFilters.EndDate:
                    if (!DateTime.TryParse(filterValue, out var endTime))
                        break;
                    endTime = endTime.ToUniversalTime();
                    query = query.Where(a => a.SheetActivities.Any(s => s.ActivityTime.Start <= endTime || s.ActivityTime.End <= endTime));
                    break;
                case GenericFilters.MissingInstructors:
                    if (!bool.TryParse(filterValue, out var shouldCheck) && !shouldCheck)
                        break;
                    query = query.Where(a => a.SheetActivities.Any(s => s.LinkedInstructors.Count() < s.AmountOfInstructors));
                    break;
            }
        }
        return query;
    }


    public override Activity Update(Activity activity)
    {
        var toUpdate = GetById(activity.Id);

        toUpdate.ActivityTypeId = activity.ActivityTypeId;
        toUpdate.Title = activity.Title;
        toUpdate.CustomerRequestId = activity.CustomerRequestId;

        toUpdate.SheetActivities.Clear();
        _activityRepository.Update(toUpdate);


        foreach (var incoming in activity.SheetActivities)
        {
            var sheet = new SheetActivity
            {
                Id = Guid.NewGuid(),
                SheetId = incoming.SheetId,
                ActivityId = toUpdate.Id,
                ClubId = toUpdate.ClubId,
                AmountOfInstructors = incoming.AmountOfInstructors,
                ActivityTime = new DateTimeRange
                {
                    Id = Guid.NewGuid(),
                    ClubId = toUpdate.ClubId,
                    Start = incoming.ActivityTime.Start,
                    End = incoming.ActivityTime.End,
                },

            };
            foreach (var instructor in incoming.LinkedInstructors)
            {
                sheet.LinkedInstructors.Add(new LinkedInstructor()
                {
                    Id = Guid.NewGuid(),
                    ClubId = toUpdate.ClubId,
                    UserIdentity = instructor.UserIdentity
                });
            }
            toUpdate.SheetActivities.Add(_sheetActivityRepository.Create(sheet));
        }
        return _entityRepository.Update(toUpdate);
    }

    public void AddInstructor(string instructorId, Guid activityId, Guid sheetActivityId)
    {
        var activity = GetById(activityId);
        var sheetActivity = activity.SheetActivities.FirstOrDefault(s => s.Id == sheetActivityId);
        if(sheetActivity == null) throw new KeyNotFoundException($"No sheet activity with id {sheetActivityId} in activity {activityId}");

        if (sheetActivity.LinkedInstructors.Any(l => l.UserIdentity == instructorId)) throw new Exception($"Sheet activity already contains user");
        sheetActivity.LinkedInstructors.Add(new LinkedInstructor()
        {
            Id = Guid.NewGuid() ,
            ClubId= sheetActivity.ClubId,
            UserIdentity = instructorId
        });
        _sheetActivityRepository.Update(sheetActivity);
    }
}
