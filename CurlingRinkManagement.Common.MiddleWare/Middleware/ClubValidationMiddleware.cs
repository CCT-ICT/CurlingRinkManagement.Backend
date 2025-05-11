using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Core.Data.DatabaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CurlingRinkManagement.Common.Api.Middleware;
public class ClubValidationMiddleware(RequestDelegate _next, ILogger<ClubValidationMiddleware> _logger)
{

    // IMessageWriter is injected into InvokeAsync
    public async Task InvokeAsync(HttpContext httpContext, IBaseRepository<Club> _clubRepository)
    {
        if (httpContext.Request.Path.Value?.Contains("swagger", StringComparison.CurrentCultureIgnoreCase) ?? false)
        {
            await _next(httpContext);
            return;
        }


        var groups = httpContext.User.Claims.Where(c => c.Type == "groups").Select(c => c.Value).ToList();
        if (!httpContext.Request.Headers.TryGetValue("X-Club-Id", out var clubId))
        {
            _logger.LogTrace("No club id header defined");
            httpContext.Response.StatusCode = 400;
            return;
        }
        var id = Guid.Parse(clubId.First()!);

        if (_clubRepository.GetAll().Any(c => c.Id == id && groups.Contains(c.ClubGroup)))
        {
            
            await _next(httpContext);
            return;
        }

        _logger.LogTrace("User has no access to club");
        httpContext.Response.StatusCode = 401;

        return;
    }
}

