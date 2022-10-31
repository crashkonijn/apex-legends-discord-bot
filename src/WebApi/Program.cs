using System.Text.Json;
using Application;
using Database;
using Discord;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tracker;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterModules(builder.Configuration);
builder.Services.RegisterBackgroundServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    Console.WriteLine(JsonSerializer.Serialize(services.GetRequiredService<IOptions<ApplicationOptions>>().Value));
    Console.WriteLine(JsonSerializer.Serialize(services.GetRequiredService<IOptions<DiscordOptions>>().Value));
    Console.WriteLine(JsonSerializer.Serialize(services.GetRequiredService<IOptions<TrackerOptions>>().Value));
    
    var databaseContext = services.GetRequiredService<StatsContext>();
    
    if (databaseContext.Database.IsRelational())
    {
        databaseContext.Database.Migrate();
    }

    await services.GetRequiredService<IImportUseCase>().Execute();
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();