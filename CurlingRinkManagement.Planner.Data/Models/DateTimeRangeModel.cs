using CurlingRinkManagement.Planner.Domain.DatabaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurlingRinkManagement.Planner.Domain.Models;

public class DateTimeRangeModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int MinutesBlockedBefore { get; set; }
    public int MinutesBlockedAfter { get; set; }

    [ForeignKey("Activity")]
    public Guid ActivityId { get; set; }

    public DateTimeRange ToDateTimeRange()
    {
        return new DateTimeRange()
        {
            Id = Id,
            Start = Start,
            End = End,
            MinutesBlockedBefore = MinutesBlockedBefore,
            MinutesBlockedAfter = MinutesBlockedAfter,
            ActivityId = ActivityId,
        };
    }

    public static DateTimeRangeModel FromDateTimeRange(DateTimeRange dateTimeRange)
    {
        return new DateTimeRangeModel()
        {
            Id = dateTimeRange.Id,
            Start = dateTimeRange.Start,
            End = dateTimeRange.End,
            MinutesBlockedBefore = dateTimeRange.MinutesBlockedBefore,
            MinutesBlockedAfter = dateTimeRange.MinutesBlockedAfter,
            ActivityId = dateTimeRange.Activity != null? dateTimeRange.Activity.Id : dateTimeRange.ActivityId,
        };
    }
}
