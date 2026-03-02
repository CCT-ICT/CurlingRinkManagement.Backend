

using CurlingRinkManagement.Common.Api.Middleware;
using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Core.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CurlingRinkManagement.Common.Api.Authorization;
public class GroupAuthorizationHandler : IAuthorizationFilter
{
    private readonly string _permissionName;
    private readonly ILogger<ClubValidationMiddleware> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IClubService _clubService;

    public GroupAuthorizationHandler(string groupName, ILogger<ClubValidationMiddleware> logger, IHttpContextAccessor httpContextAccessor, IClubService clubService)
    {
        _permissionName = groupName;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _clubService = clubService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            context.Result = new ForbidResult();
            return;
        }
        var groups = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "groups").Select(c => c.Value).ToList();
        if ( !_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-Club-Id", out var clubId))
        {
            _logger.LogTrace("No club id header defined");
            context.Result = new ForbidResult();
            return;
        }
        var id = Guid.Parse(clubId.First()!);
        var club = _clubService.GetClubById(id);

        if (!groups.Any(c => c == $"{club.ClubAbbriviation}-{_permissionName}"))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}

