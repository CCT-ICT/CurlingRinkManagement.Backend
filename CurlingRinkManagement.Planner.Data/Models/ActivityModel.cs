using CurlingRinkManagement.Planner.Data.DatabaseModels;

namespace CurlingRinkManagement.Planner.Data.Models;
public class ActivityModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public List<SheetActivityModel> SheetActivities { get; set; } = [];
    public Guid ActivityTypeId { get; set; }
    public Guid? CustomerRequestId { get; set; }

    public static ActivityModel FromActivity(Activity activity)
    {
        return new ActivityModel()
        {
            Id = activity.Id,
            Title = activity.Title,
            SheetActivities = activity.SheetActivities.Select(SheetActivityModel.FromSheetActivity).ToList(),
            ActivityTypeId = activity.ActivityTypeId,
            CustomerRequestId = activity.CustomerRequestId
        };
    }

    public Activity ToActivity()
    {
        return new Activity()
        {
            Id = Id,
            Title = Title,
            SheetActivities = SheetActivities.Select(s => s.ToSheetActivity()).ToList(),
            ActivityTypeId = ActivityTypeId,
            CustomerRequestId = CustomerRequestId
        };
    }

}
