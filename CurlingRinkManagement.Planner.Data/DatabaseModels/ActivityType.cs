
using CurlingRinkManagement.Planner.Domain.Interfaces;

namespace CurlingRinkManagement.Planner.Domain.DatabaseModels;

public class ActivityType : IDatabaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Type { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int RecommendedMinutesBlockedBefore { get; set; }
    public int RecommendedMinutesBlockedAfter { get; set; }
}

