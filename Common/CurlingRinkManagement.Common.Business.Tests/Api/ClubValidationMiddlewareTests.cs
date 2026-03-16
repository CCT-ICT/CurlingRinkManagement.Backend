using System.Security.Claims;
using CurlingRinkManagement.Common.Api.Middleware;
using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Core.Data.DatabaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CurlingRinkManagement.Tests.Middleware;

[TestClass]
public class ClubValidationMiddlewareTests
{
    private Mock<ILogger<ClubValidationMiddleware>> _loggerMock = null!;
    private Mock<IBaseRepository<Club>> _repoMock = null!;
    private bool _nextCalled;

    [TestInitialize]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<ClubValidationMiddleware>>();
        _repoMock = new Mock<IBaseRepository<Club>>();
        _nextCalled = false;
    }

    private ClubValidationMiddleware CreateMiddleware()
    {
        RequestDelegate next = (ctx) =>
        {
            _nextCalled = true;
            return Task.CompletedTask;
        };

        return new ClubValidationMiddleware(next, _loggerMock.Object);
    }

    private static DefaultHttpContext CreateHttpContext()
    {
        return new DefaultHttpContext();
    }

    [TestMethod]
    public async Task InvokeAsync_NoClubHeader_Returns400()
    {
        var middleware = CreateMiddleware();
        var context = CreateHttpContext();

        await middleware.InvokeAsync(context, _repoMock.Object);

        Assert.AreEqual(400, context.Response.StatusCode);
        Assert.IsFalse(_nextCalled);
    }

    [TestMethod]
    public async Task InvokeAsync_UserHasAccess_CallsNext()
    {
        var middleware = CreateMiddleware();
        var context = CreateHttpContext();

        var clubId = Guid.NewGuid();
        var group = "group1";

        context.Request.Headers["X-Club-Id"] = clubId.ToString();

        context.User = new ClaimsPrincipal(
            new ClaimsIdentity(
            [
                new Claim("groups", group)
            ]));

        var clubs = new List<Club>
        {
            new Club
            {
                Id = clubId,
                ClubGroup = group
            }
        }.AsQueryable();

        _repoMock.Setup(r => r.GetAll()).Returns(clubs);

        await middleware.InvokeAsync(context, _repoMock.Object);

        Assert.IsTrue(_nextCalled);
    }

    [TestMethod]
    public async Task InvokeAsync_UserWithoutAccess_Returns401()
    {
        var middleware = CreateMiddleware();
        var context = CreateHttpContext();

        var clubId = Guid.NewGuid();

        context.Request.Headers["X-Club-Id"] = clubId.ToString();

        context.User = new ClaimsPrincipal(
            new ClaimsIdentity(
            [
                new Claim("groups", "group1")
            ]));

        var clubs = new List<Club>
        {
            new Club
            {
                Id = clubId,
                ClubGroup = "different-group"
            }
        }.AsQueryable();

        _repoMock.Setup(r => r.GetAll()).Returns(clubs);

        await middleware.InvokeAsync(context, _repoMock.Object);

        Assert.AreEqual(401, context.Response.StatusCode);
        Assert.IsFalse(_nextCalled);
    }

    [TestMethod]
    public async Task InvokeAsync_ClubNotFound_Returns401()
    {
        var middleware = CreateMiddleware();
        var context = CreateHttpContext();

        context.Request.Headers["X-Club-Id"] = Guid.NewGuid().ToString();

        context.User = new ClaimsPrincipal(
            new ClaimsIdentity(new[]
            {
                new Claim("groups", "group1")
            }));

        _repoMock.Setup(r => r.GetAll()).Returns(new List<Club>().AsQueryable());

        await middleware.InvokeAsync(context, _repoMock.Object);

        Assert.AreEqual(401, context.Response.StatusCode);
        Assert.IsFalse(_nextCalled);
    }
}