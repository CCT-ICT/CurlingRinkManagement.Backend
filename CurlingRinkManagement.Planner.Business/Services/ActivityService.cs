using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Planner.Data.DatabaseModels;
using CurlingRinkManagement.Planner.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurlingRinkManagement.Planner.Business.Services;

public class ActivityService(IClubRepository<Activity> _activityRepository) : IActivityService
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
        var activity = _activityRepository.GetAll().Include(a => a.SheetActivities).ThenInclude(s => s.ActivityTime).Include(a => a.SheetActivities).FirstOrDefault(x => x.Id == id);

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

    public Activity Update(Activity activity)
    {
        var toUpdate = GetById(activity.Id);

        toUpdate.ActivityTypeId = activity.ActivityTypeId;
        toUpdate.Title = activity.Title;
        toUpdate.CustomerRequestId = activity.CustomerRequestId;
        
        var newSheets = activity.SheetActivities.Where(s => !toUpdate.SheetActivities.Any(s2 => s.SheetId == s2.SheetId)).ToList();
        var removedsSheets = toUpdate.SheetActivities.Where(s => !activity.SheetActivities.Any(s2 => s.SheetId == s2.SheetId)).ToList();
        var unchangedSheets = toUpdate.SheetActivities.Where(s => activity.SheetActivities.Any(s2 => s.SheetId == s2.SheetId)).ToList();

        foreach (var sheet in removedsSheets)
        {
            toUpdate.SheetActivities.Remove(sheet);
        }
        foreach (var sheet in newSheets)
        {
            toUpdate.SheetActivities.Add(sheet);
        }
        foreach (var sheet in unchangedSheets)
        {
            sheet.ActivityTime = activity.SheetActivities.First(s => s.SheetId == sheet.SheetId).ActivityTime;
        }

        var clubId = _activityRepository.GetClubId();

        foreach (var sheet in toUpdate.SheetActivities)
        {
            sheet.ClubId = clubId;
            sheet.ActivityTime.ClubId = clubId;
        }

        return _activityRepository.Update(toUpdate);
    }
}
