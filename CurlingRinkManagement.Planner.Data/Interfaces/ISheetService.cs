
using CurlingRinkManagement.Planner.Data.DatabaseModels;

namespace CurlingRinkManagement.Planner.Data.Interfaces;

public interface ISheetService
{
    void Delete(Guid sheetId);
    List<Sheet> GetAll();
    Sheet Update(Sheet sheet);
    Sheet Create(Sheet sheet);
}

