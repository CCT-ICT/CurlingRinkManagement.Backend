using CurlingRinkManagement.Planner.Data.Interfaces;
using CurlingRinkManagement.Planner.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurlingRinkManagement.Planner.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class ActivityController(IActivityService _activityService) : ControllerBase
{

    [HttpPut]
    [Route("{activityId?}/{sheetActivityId?}/{instructorId?}")]
    public IActionResult AddInstructor(Guid activityId, Guid sheetActivityId, string instructorId)
    {
        try
        {
            _activityService.AddInstructor(instructorId, activityId, sheetActivityId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult Get([FromQuery] int? page, [FromQuery] int? amount, [FromQuery] string[]? filters, [FromQuery] string[]? filterValues)
    {
        try
        {
            var result = _activityService.GetAll(page, amount, filters, filterValues);
            var converted = result.Select(ActivityModel.FromActivity);
            return Ok(converted);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("{activityId?}")]
    public IActionResult GetById(Guid activityId)
    {
        try
        {
            var activity = _activityService.GetById(activityId);
            var converted = ActivityModel.FromActivity(activity);
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

    [HttpDelete]
    [Route("{activityId?}")]
    public IActionResult Delete(Guid activityId)
    {
        try
        {
            _activityService.Delete(activityId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }




}
