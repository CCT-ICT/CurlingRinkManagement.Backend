using CurlingRinkManagement.Planner.Domain.DatabaseModels;

namespace CurlingRinkManagement.Planner.Domain.Models;
public class ActivityModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public List<DateTimeRangeModel> PlannedDates { get; set; } = [];
    public List<Guid> SheetIds { get; set; } = [];
    public Guid ActivityTypeId { get; set; }

    public static ActivityModel FromActivity(Activity activity)
    {
        return new ActivityModel()
        {
            Id = activity.Id,
            Title = activity.Title,
            PlannedDates = activity.PlannedDates.Select(DateTimeRangeModel.FromDateTimeRange).ToList(),
            SheetIds = activity.Sheets.Select(s => s.SheetId).ToList(),
            ActivityTypeId = activity.ActivityTypeId,
        };
    }

    public Activity ToActivity()
    {
        return new Activity()
        {
            Id = Id,
            Title = Title,
            PlannedDates = PlannedDates.Select(d => d.ToDateTimeRange()).ToList(),
            Sheets = SheetIds.Select(s => new SheetActivity() { Id = Guid.NewGuid(), SheetId = s, ActivityId = Id}).ToList(),
            ActivityTypeId = ActivityTypeId,
        };
    }

}
