using CurlingRinkManagement.Planner.Domain.DatabaseModels;
using CurlingRinkManagement.Planner.Domain.Interfaces;

namespace CurlingRinkManagement.Planner.Business.Services;

public class SheetService(IRepository<Sheet> sheetRepository) : ISheetService 
{
    private readonly IRepository<Sheet> _sheetRepository = sheetRepository;

    public List<Sheet> GetAll()
    {
        return _sheetRepository.GetAll().ToList();
    }
}
