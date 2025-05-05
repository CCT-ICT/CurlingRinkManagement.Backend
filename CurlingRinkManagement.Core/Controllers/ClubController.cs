using CurlingRinkManagement.Core.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CurlingRinkManagement.Core.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class ClubController(IClubService _clubService) : ControllerBase
{
    [HttpGet]
    public IActionResult Get(Guid sheetId, [FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var groups = User.Claims.Where(c => c.Type == "Group").Select(c => c.Value);
        try
        {
            var clubs = _clubService.GetClubs([.. groups]);
            return Ok(clubs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
