
using CurlingRinkManagement.Common.Data.Database;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;

public class ActivityType : IClubEntity
{
    public Guid ClubId { get; set; } = Guid.Empty;
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Type { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int RecommendedMinutesBlockedBefore { get; set; }
    public int RecommendedMinutesBlockedAfter { get; set; }
}

