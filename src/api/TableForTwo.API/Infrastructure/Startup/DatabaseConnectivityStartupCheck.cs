using Microsoft.Extensions.Options;
using Npgsql;
using TableForTwo.API.Infrastructure.Configuration.Options;

namespace TableForTwo.API.Infrastructure.Startup;

public sealed class DatabaseConnectivityStartupCheck(
    IOptions<PostgresOptions> postgresOptions,
    ILogger<DatabaseConnectivityStartupCheck> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Checking PostgreSQL connectivity during startup.");

        try
        {
            await using var connection = new NpgsqlConnection(postgresOptions.Value.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            logger.LogInformation("PostgreSQL connectivity verified during startup.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "PostgreSQL connectivity check failed during startup.");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
