using CurlingRinkManagement.Core.Data.DatabaseModels;
using CurlingRinkManagement.Core.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurlingRinkManagement.Core.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class UserController(IUserService _userService) : ControllerBase
{
    [HttpGet]
    [Route("{club}")]
    public async Task<IActionResult> Get(string club, [FromQuery] string? search)
    {
        var groups = User.Claims.Where(c => c.Type == "groups").Select(c => c.Value);
        if (!groups.Contains(club)) return Unauthorized("No acces to club");

        try
        {
            var clubs = await _userService.GetUsers(club, search);
            return Ok(clubs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
