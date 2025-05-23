using CurlingRinkManagement.Common.Business.Services;
using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Planner.Data.DatabaseModels;
using CurlingRinkManagement.Planner.Data.Interfaces;

namespace CurlingRinkManagement.Planner.Business.Services;

public class SheetService(IClubRepository<Sheet> sheetRepository) : BaseService<Sheet>(sheetRepository), ISheetService 
{
    private readonly IClubRepository<Sheet> _sheetRepository = sheetRepository;


    public override Sheet Update(Sheet sheet)
    {
        var sheetToUpdate = GetById(sheet.Id);

        sheetToUpdate.Order = sheet.Order;
        sheetToUpdate.Name = sheet.Name;

        return _sheetRepository.Update(sheetToUpdate);
    }
}
