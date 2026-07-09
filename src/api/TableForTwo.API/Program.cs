using Scalar.AspNetCore;
using TableForTwo.API.Infrastructure.Configuration;
using TableForTwo.API.Infrastructure.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationOptions(builder.Configuration);
builder.Services.AddHostedService<DatabaseConnectivityStartupCheck>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();

app.MapControllers();

app.Run();