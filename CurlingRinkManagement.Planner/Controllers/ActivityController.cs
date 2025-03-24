using CurlingRinkManagement.Planner.Domain.Interfaces;
using CurlingRinkManagement.Planner.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurlingRinkManagement.Planner.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class ActivityController(IActivityService activityService) : ControllerBase
{
    private readonly IActivityService _activityService = activityService;

    [HttpGet]
    [Route("{sheetId?}")]
    public IActionResult Get(Guid sheetId, [FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        try
        {
            var activities = _activityService.GetAllOnSheet(sheetId, start, end);
            var converted = activities.Select(ActivityModel.FromActivity);
            return Ok(converted);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public IActionResult Update(ActivityModel activity)
    {
        try
        {
            var result = _activityService.Update(activity.ToActivity());
            var converted = ActivityModel.FromActivity(result);
            return Ok(converted);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Create(ActivityModel activity)
    {
        try
        {
            var result = _activityService.Create(activity.ToActivity());
            var converted = ActivityModel.FromActivity(result);
            return Ok(converted);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}
