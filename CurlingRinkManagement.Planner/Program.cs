using CurlingRinkManagement.Planner.Business.Database;
using CurlingRinkManagement.Planner.Business.Services;
using CurlingRinkManagement.Planner.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<DataContext>(opt =>
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<ISheetService, SheetService>();
builder.Services.AddScoped<IActivityTypeService, ActivityTypeService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

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

app.Run();
