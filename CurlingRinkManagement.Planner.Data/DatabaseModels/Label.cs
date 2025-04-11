using CurlingRinkManagement.Planner.Domain.Interfaces;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;

public class Label : IDatabaseEntity
{
    public Guid Id { get; set; } 
    public string Title { get; set; } = string.Empty;
}