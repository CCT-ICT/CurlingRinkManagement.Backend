using CurlingRinkManagement.Planner.Domain.DatabaseModels;
using CurlingRinkManagement.Planner.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurlingRinkManagement.Planner.Business.Services;

public class ActivityService(IRepository<Activity> _activityRepository) : IActivityService
{
    public Activity Create(Activity activity)
    {
        return _activityRepository.Create(activity);
    }

    public void Delete(Guid id)
    {
        var activity = GetById(id);
        _activityRepository.Delete(activity);
    }

    public Activity GetById(Guid id)
    {
        var activity = _activityRepository.GetAll().Include(a => a.PlannedDates).Include(a => a.Sheets).FirstOrDefault(x => x.Id == id);

        if (activity == null)
            throw new KeyNotFoundException($"Activity with id {id} does not exist");
        return activity;
    }

    public List<Activity> GetAllOnSheet(Guid sheetId, DateTime start, DateTime end)
    {
        var activitiesQuery = _activityRepository.GetAll().Include(a => a.PlannedDates).Include(a => a.ActivityType).Include(a => a.Sheets)
            .Where(a => a.Sheets.Any(s => s.SheetId == sheetId))
            .Where(a => a.PlannedDates.Any(d => (d.Start.AddMinutes(-d.MinutesBlockedBefore) >= start && d.Start.AddMinutes(-d.MinutesBlockedBefore) <= end) ||
                                                (d.End.AddMinutes(d.MinutesBlockedAfter) >= start && d.End.AddMinutes(d.MinutesBlockedAfter) <= end)));

        return activitiesQuery.ToList();
    }

    public Activity Update(Activity activity)
    {
        var toUpdate = GetById(activity.Id);

        toUpdate.ActivityTypeId = activity.ActivityTypeId;
        toUpdate.PlannedDates = activity.PlannedDates;
        toUpdate.Title = activity.Title;
        
        var newSheets = activity.Sheets.Where(s => !toUpdate.Sheets.Any(s2 => s.SheetId == s2.SheetId)).ToList();
        var removedsSheets = toUpdate.Sheets.Where(s => !activity.Sheets.Any(s2 => s.SheetId == s2.SheetId)).ToList();

        foreach (var sheet in removedsSheets)
        {
            toUpdate.Sheets.Remove(sheet);
        }
        foreach (var sheet in newSheets)
        {
            toUpdate.Sheets.Add(sheet);
        }

        return _activityRepository.Update(toUpdate);
    }
}
