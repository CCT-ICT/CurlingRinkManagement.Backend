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

    public Sheet GetById(Guid sheetId)
    {
        var sheet = _sheetRepository.GetAll().FirstOrDefault(s => s.Id == sheetId);
        return sheet == null ? throw new KeyNotFoundException($"No sheet found with id: {sheetId}") : sheet;
    }

    public Sheet Create(Sheet sheet)
    {
        return _sheetRepository.Create(sheet);
    }

    public void Delete(Guid sheetId)
    {
        var sheet = GetById(sheetId);
        _sheetRepository.Delete(sheet);
    }

    public Sheet Update(Sheet sheet)
    {
        var sheetToUpdate = GetById(sheet.Id);

        sheetToUpdate.Order = sheet.Order;
        sheetToUpdate.Name = sheet.Name;

        return _sheetRepository.Update(sheetToUpdate);
    }
}
