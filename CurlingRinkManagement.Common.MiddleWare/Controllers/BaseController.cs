
using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Common.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.Common;

namespace CurlingRinkManagement.Common.Api.Controllers;
public class BaseController<Entity>(IBaseService<Entity> _baseService) : Controller where Entity : class, IClubEntity
{
    [HttpGet]
    public IActionResult Get([FromQuery] int? page, [FromQuery] int? amount, [FromQuery] string[]? filters, [FromQuery] string[]? filterValues)
    {
        try
        {
            var result = _baseService.GetAll(page, amount, filters, filterValues);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("Amount")]
    public IActionResult GetAmount([FromQuery] string[]? filters, [FromQuery] string[]? filterValues)
    {
        try
        {
            var result = _baseService.GetAmount(filters, filterValues);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Post(Entity entity)
    {
        try
        {
            var result = _baseService.Create(entity);
            return Ok(result);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException != null && e.InnerException is PostgresException postgres && postgres.SqlState.Equals("23505"))
                return StatusCode(StatusCodes.Status409Conflict);
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public IActionResult Put(Entity entity)
    {
        try
        {
            var result = _baseService.Update(entity);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{sheetId?}")]
    public IActionResult Delete(Guid entityId)
    {
        try
        {
            _baseService.Delete(entityId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("{sheetId?}")]
    public IActionResult GetById(Guid entityId)
    {
        try
        {
            var entity = _baseService.GetById(entityId);
            return Ok(entity);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
