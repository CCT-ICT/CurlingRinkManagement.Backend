using CurlingRinkManagement.Common.Data.Database;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;
public class DateTimeRange : IClubEntity
{
    public Guid ClubId { get; set; } = Guid.Empty;
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public bool TimesEqual(DateTimeRange dateTimeRange)
    {
        return dateTimeRange.Start == Start && dateTimeRange.End == End;
    }
}

