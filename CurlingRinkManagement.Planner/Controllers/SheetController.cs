using CurlingRinkManagement.Planner.Data.DatabaseModels;
using CurlingRinkManagement.Planner.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurlingRinkManagement.Planner.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class SheetController(ISheetService sheetService) : Controller
{
    private readonly ISheetService _sheetService = sheetService;

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var result = _sheetService.GetAll();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Post(Sheet sheet)
    {
        try
        {
            var result = _sheetService.Create(sheet);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public IActionResult Put(Sheet sheet)
    {
        try
        {
            var result = _sheetService.Update(sheet);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{sheetId?}")]
    public IActionResult Delete(Guid sheetId)
    {
        try
        {
            _sheetService.Delete(sheetId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
