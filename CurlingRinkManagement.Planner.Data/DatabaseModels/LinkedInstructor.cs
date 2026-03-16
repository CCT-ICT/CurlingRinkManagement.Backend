using CurlingRinkManagement.Common.Data.Database;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;
public class LinkedInstructor : IClubEntity
{
    public Guid Id { get; set; }
    public Guid ClubId { get; set; }
    public string UserIdentity { get; set; } = string.Empty;
}

