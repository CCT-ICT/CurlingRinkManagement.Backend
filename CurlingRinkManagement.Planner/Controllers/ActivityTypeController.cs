using CurlingRinkManagement.Planner.Domain.DatabaseModels;
using CurlingRinkManagement.Planner.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurlingRinkManagement.Planner.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class ActivityTypeController(IActivityTypeService activityTypeService) : Controller
{
    private readonly IActivityTypeService _activityTypeService = activityTypeService;

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var result = _activityTypeService.GetAll();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Post(ActivityType activityType)
    {
        try
        {
            var result = _activityTypeService.Create(activityType);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public IActionResult Put(ActivityType activityType)
    {
        try
        {
            var result = _activityTypeService.Update(activityType);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

