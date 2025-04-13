using CurlingRinkManagement.Planner.Data.DatabaseModels;
using CurlingRinkManagement.Planner.Data.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;

public class Activity : IDatabaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;

    //References
    public List<DateTimeRange> PlannedDates { get; set; } = [];
    public List<SheetActivity> Sheets { get; set; } = [];

    [ForeignKey("ActivityType")]
    public Guid ActivityTypeId { get; set; }
    public ActivityType? ActivityType { get; set; } = null;

    [ForeignKey("CustomerRequest")]
    public Guid? CustomerRequestId { get; set; }
    public CustomerRequest? CustomerRequest { get; set; } = null;

}

