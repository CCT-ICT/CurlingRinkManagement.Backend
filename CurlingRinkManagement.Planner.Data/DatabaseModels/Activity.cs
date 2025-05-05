using CurlingRinkManagement.Common.Data.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;

public class Activity : IClubEntity
{
    public Guid ClubId { get; set; } = Guid.Empty;
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

