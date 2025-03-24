using CurlingRinkManagement.Base.Data.DatabaseModels;
using CurlingRinkManagement.Base.Data.Interfaces;
using CurlingRinkManagement.Base.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurlingRinkManagement.Base.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(ILogger<AuthController> _logger, IAuthenticationService _authenticationService) : ControllerBase
    {

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            try
            {
                var token = _authenticationService.Register(registerModel);
                return Ok(token);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Check()
        {
            return Ok();
        }
    }
}
