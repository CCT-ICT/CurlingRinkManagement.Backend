
using CurlingRinkManagement.Planner.Data.DatabaseModels;

namespace CurlingRinkManagement.Planner.Data.Interfaces;

public interface ISheetService
{
    List<Sheet> GetAll();
}

