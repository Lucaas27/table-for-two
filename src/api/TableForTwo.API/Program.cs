using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using TableForTwo.API.Infrastructure.Configuration;
using TableForTwo.API.Infrastructure.Health;
using TableForTwo.API.Infrastructure.Http;
using TableForTwo.API.Infrastructure.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationOptions(builder.Configuration);
builder.Services.AddHostedService<DatabaseConnectivityStartupCheck>();
builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "One or more validation errors occurred."
        };

        ProblemDetailsEnricher.Enrich(context.HttpContext, problemDetails);
        return new BadRequestObjectResult(problemDetails);
    };
});
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        ProblemDetailsEnricher.Enrich(context.HttpContext, context.ProblemDetails);
    };
});
builder.Services.AddHealthChecks()
    .AddCheck("api", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy("API is ready."))
    .AddCheck<PostgresHealthCheck>("postgres");
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.IncludeScopes = true;
    options.SingleLine = true;
    options.TimestampFormat = "HH:mm:ss ";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<RequestCorrelationMiddleware>();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseAuthorization();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        var payload = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.ToDictionary(
                entry => entry.Key,
                entry => new
                {
                    status = entry.Value.Status.ToString(),
                    description = entry.Value.Description
                })
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
});
app.MapControllers();

app.Run();

public partial class Program
{
}
