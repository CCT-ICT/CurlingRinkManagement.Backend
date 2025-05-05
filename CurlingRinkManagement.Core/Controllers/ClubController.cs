using CurlingRinkManagement.Core.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurlingRinkManagement.Core.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class ClubController(IClubService _clubService) : ControllerBase
{
    [HttpGet]
    public IActionResult Get(Guid sheetId, [FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var u = User;
        /*try
        {
            var activities = _clubService.GetClubs();
            var converted = activities.Select(_clubService.FromActivity);
            return Ok(converted);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }*/
        return Ok();
    }
}
