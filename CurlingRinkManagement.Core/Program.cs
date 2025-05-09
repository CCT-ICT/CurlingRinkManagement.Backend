using CurlingRinkManagement.Common.Api.Extensions;
using CurlingRinkManagement.Core.Business.Services;
using CurlingRinkManagement.Core.Data.Database;
using CurlingRinkManagement.Core.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBaseDatabase<CoreDataContext>(builder.Configuration);
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddGenericAuthentication(builder.Configuration);

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

// Configure the HTTP request pipeline.
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
