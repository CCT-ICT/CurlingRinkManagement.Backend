using CurlingRinkManagement.Common.Data.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;

public class Tag : IClubEntity
{
    public Guid ClubId { get; set; } = Guid.Empty;
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;

    [ForeignKey("Parent")]
    public Guid? ParentId { get; set; }
    public Tag? Parent { get; set; }
}