using CurlingRinkManagement.Planner.Domain.DatabaseModels;

namespace CurlingRinkManagement.Planner.Domain.Interfaces;

public interface ISheetService
{
    List<Sheet> GetAll();
}

