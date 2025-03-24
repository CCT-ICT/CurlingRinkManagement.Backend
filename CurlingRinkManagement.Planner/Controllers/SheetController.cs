using CurlingRinkManagement.Planner.Domain.Interfaces;
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
}
