using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Planner.Data.DatabaseModels;
using CurlingRinkManagement.Planner.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CurlingRinkManagement.Planner.Business.Services;

public class ActivityService(IClubRepository<Activity> _activityRepository, IClubRepository<SheetActivity> _sheetActivityRepository) : IActivityService
{
    public Activity Create(Activity activity)
    {
        var clubId = _activityRepository.GetClubId();

        foreach (var sheet in activity.SheetActivities)
        {
            sheet.ClubId = clubId;
            sheet.ActivityTime.ClubId = clubId;
        }

        return _activityRepository.Create(activity);
    }

    public void Delete(Guid id)
    {
        var activity = GetById(id);
        _activityRepository.Delete(activity);
    }

    public Activity GetById(Guid id)
    {
        var activity = _activityRepository.GetAll().Include(a => a.SheetActivities).ThenInclude(s => s.ActivityTime).Include(a => a.SheetActivities).ThenInclude(s => s.LinkedInstructors).FirstOrDefault(x => x.Id == id);

        if (activity == null)
            throw new KeyNotFoundException($"Activity with id {id} does not exist");
        return activity;
    }

    public List<Activity> GetAllOnSheet(Guid sheetId, DateTime start, DateTime end)
    {
        var activitiesQuery = _activityRepository.GetAll().Include(a => a.SheetActivities).ThenInclude(s => s.ActivityTime).Include(a => a.ActivityType).Include(a => a.SheetActivities)
            .Where(a => a.SheetActivities.Any(s => s.SheetId == sheetId))
            .Where(a => a.SheetActivities.Any(d => (d.ActivityTime.Start.AddMinutes(-d.ActivityTime.MinutesBlockedBefore) >= start && d.ActivityTime.Start.AddMinutes(-d.ActivityTime.MinutesBlockedBefore) <= end) ||
                                                (d.ActivityTime.End.AddMinutes(d.ActivityTime.MinutesBlockedAfter) >= start && d.ActivityTime.End.AddMinutes(d.ActivityTime.MinutesBlockedAfter) <= end)));

        return activitiesQuery.ToList();
    }

    //TODO This is terrible. Find a better way
    public Activity Update(Activity activity)
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
                ActivityTime = new DateTimeRange
                {
                    Id = Guid.NewGuid(),
                    ClubId = toUpdate.ClubId,
                    Start = incoming.ActivityTime.Start,
                    End = incoming.ActivityTime.End,
                    MinutesBlockedBefore = incoming.ActivityTime.MinutesBlockedBefore,
                    MinutesBlockedAfter = incoming.ActivityTime.MinutesBlockedAfter
                },

            };
            foreach(var instructor in incoming.LinkedInstructors)
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
        return _activityRepository.Update(toUpdate);
    }
}
