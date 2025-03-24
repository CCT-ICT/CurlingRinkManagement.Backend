using CurlingRinkManagement.Planner.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurlingRinkManagement.Planner.Domain.DatabaseModels;
public class DateTimeRange : IDatabaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int MinutesBlockedBefore { get; set; }
    public int MinutesBlockedAfter { get; set; }

    [ForeignKey("Activity")]
    public Guid ActivityId { get; set; }

    public Activity? Activity { get; set; }
}

