using CurlingRinkManagement.Common.Api.Extensions;
using CurlingRinkManagement.Core.Business.Services;
using CurlingRinkManagement.Core.Data.Database;
using CurlingRinkManagement.Core.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBaseDatabase<CoreDataContext>(builder.Configuration);
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddGenericAuthentication(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
