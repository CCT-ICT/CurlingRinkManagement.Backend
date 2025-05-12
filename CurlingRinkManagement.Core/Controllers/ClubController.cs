using CurlingRinkManagement.Core.Data.DatabaseModels;
using CurlingRinkManagement.Core.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurlingRinkManagement.Core.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class ClubController(IClubService _clubService) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var groups = User.Claims.Where(c => c.Type == "groups").Select(c => c.Value);
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

    [HttpPost]
    public IActionResult Create(Club club)
    {
        try
        {
            var clubs = _clubService.Create(club);
            return Ok(clubs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
