using CurlingRinkManagement.Common.Data.Database;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;

public class Sheet : IClubEntity
{
    public Guid ClubId { get; set; } = Guid.Empty;
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; } = 0;

}

