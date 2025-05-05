using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace CurlingRinkManagement.Common.Api.Middleware;
public class MyClubMiddleware
{
    private readonly RequestDelegate _next;

    public MyClubMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // IMessageWriter is injected into InvokeAsync
    public async Task InvokeAsync(HttpContext httpContext)
    {
        
        await _next(httpContext);
    }
}

