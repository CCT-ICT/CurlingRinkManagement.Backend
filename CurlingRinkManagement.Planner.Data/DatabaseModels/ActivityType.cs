
using CurlingRinkManagement.Common.Data.Database;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;

public class ActivityType : IClubEntity
{
    public Guid ClubId { get; set; } = Guid.Empty;
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Type { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int AmountOfInstructors { get; set; }
    public CalculationType InstructorCalculationType { get; set; }
    public int PerSelectedValue { get; set; }
}

