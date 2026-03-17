using CurlingRinkManagement.Common.Api.Extensions;
using CurlingRinkManagement.Common.Api.Middleware;
using CurlingRinkManagement.Planner.Business.Services;
using CurlingRinkManagement.Planner.Data.Database;
using CurlingRinkManagement.Planner.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGenericAuthentication(builder.Configuration);
builder.Services.AddDatabase<PlannerDataContext>(builder.Configuration);
builder.Services.AddCoreDatabase(builder.Configuration);

builder.Services.AddScoped<IActivityService, ActivityService>()
    .AddScoped<ISheetService, SheetService>()
    .AddScoped<IActivityTypeService, ActivityTypeService>()
    .AddScoped<IContactService, ContactService>()
    .AddScoped<ICustomerRequestService, CustomerRequestService>();


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
