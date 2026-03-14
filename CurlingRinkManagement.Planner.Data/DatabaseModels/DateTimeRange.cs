using CurlingRinkManagement.Common.Data.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;
public class DateTimeRange : IClubEntity
{
    public Guid ClubId { get; set; } = Guid.Empty;
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int MinutesBlockedBefore { get; set; }
    public int MinutesBlockedAfter { get; set; }

    public bool TimesEqual(DateTimeRange dateTimeRange)
    {
        return dateTimeRange.Start == Start && dateTimeRange.End == End && 
            dateTimeRange.MinutesBlockedBefore == MinutesBlockedBefore && dateTimeRange.MinutesBlockedAfter == MinutesBlockedAfter;
    }
}

