using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Planner.Data.DatabaseModels;
using CurlingRinkManagement.Planner.Data.Interfaces;

namespace CurlingRinkManagement.Planner.Business.Services;

public class SheetService(IClubRepository<Sheet> sheetRepository) : ISheetService 
{
    private readonly IClubRepository<Sheet> _sheetRepository = sheetRepository;

    public List<Sheet> GetAll()
    {
        return _sheetRepository.GetAll().ToList();
    }
}
