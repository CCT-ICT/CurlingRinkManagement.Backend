using CurlingRinkManagement.Common.Api.Extensions;
using CurlingRinkManagement.Common.Api.Middleware;
using CurlingRinkManagement.Planner.Business.Services;
using CurlingRinkManagement.Planner.Data.Database;
using CurlingRinkManagement.Planner.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGenericAuthentication(builder.Configuration);
builder.Services.AddDatabase<PlannerDataContext>(builder.Configuration);
builder.Services.AddCoreDatabase(builder.Configuration);

builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<ISheetService, SheetService>();
builder.Services.AddScoped<IActivityTypeService, ActivityTypeService>();


//Make Cors stricter at some point
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Do this after authorization
app.UseMiddleware<ClubValidationMiddleware>();

app.Run();
