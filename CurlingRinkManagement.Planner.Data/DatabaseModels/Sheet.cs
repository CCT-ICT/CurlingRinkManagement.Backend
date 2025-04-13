using CurlingRinkManagement.Planner.Data.Interfaces;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;

public class Sheet : IDatabaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; } = 0;

}

