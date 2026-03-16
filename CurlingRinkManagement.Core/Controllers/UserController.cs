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

    [HttpGet]
    [Route("myId")]
    public async Task<IActionResult> GetMyId()
    {
        var groups = User.Claims.Where(c => c.Type == "groups").Select(c => c.Value);
        //We use email as one of the only searchable tokens. Later in the service we will check it with the uid
        var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
        var uid = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        if (email == null || uid == null) return Unauthorized("Claim is not valid");

        try
        {

            var userId = await _userService.GetMyId(groups.First(), email, uid);
            return Ok(userId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
