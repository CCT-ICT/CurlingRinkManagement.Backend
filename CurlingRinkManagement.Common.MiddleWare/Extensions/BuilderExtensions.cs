using CurlingRinkManagement.Common.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;


namespace CurlingRinkManagement.Common.Api.Extensions;
public static class BuilderExtensions
{

    public static IServiceCollection AddDatabase<DataContext>(this IServiceCollection services, IConfiguration configuration) where DataContext : DbContext
    {
        services.AddDbContextPool<DataContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("Database")));

        services.AddScoped<DbContext, DataContext>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped(typeof(IClubRepository<>), typeof(ClubRepository<>));
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        return services;
    }

    public static IServiceCollection AddBaseDatabase<DataContext>(this IServiceCollection services, IConfiguration configuration) where DataContext : DbContext
    {
        services.AddDbContextPool<DataContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("Database")));

        services.AddScoped<DbContext, DataContext>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        return services;

    }

    public static IServiceCollection AddGenericAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer("Bearer", options =>
        {
            var oidcConfig = configuration.GetSection("OpenIDConnectSettings");
            options.IncludeErrorDetails = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                //define which claim requires to check
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = oidcConfig["Issuer"],
                IssuerSigningKey = new X509SecurityKey(X509Certificate2.CreateFromPem(oidcConfig["ClientSecret"]!)),
            };
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    // Log the error
                    Console.WriteLine($"Authentication failed: {context.Exception.Message}");

                    // For development only - expose the error details
                    if (context.Exception is SecurityTokenExpiredException)
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }

                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    // Log additional information when challenge occurs
                    Console.WriteLine($"OnChallenge: {context.Error}, {context.ErrorDescription}");
                    return Task.CompletedTask;
                }
            };
        });

        var requireAuthPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

        services.AddAuthorizationBuilder()
            .SetFallbackPolicy(requireAuthPolicy);

        return services;
    }
}

