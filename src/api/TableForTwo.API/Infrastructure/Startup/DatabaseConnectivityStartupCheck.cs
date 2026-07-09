using Microsoft.Extensions.Options;
using Npgsql;
using TableForTwo.API.Infrastructure.Configuration;
using TableForTwo.API.Infrastructure.Configuration.Options;

namespace TableForTwo.API.Infrastructure.Startup;

public sealed class DatabaseConnectivityStartupCheck(
    IOptions<PostgresOptions> postgresOptions,
    ILogger<DatabaseConnectivityStartupCheck> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(postgresOptions.Value.ConnectionString);
        await connection.OpenAsync(cancellationToken);
        await connection.CloseAsync();

        logger.LogInformation("PostgreSQL connectivity verified during startup.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
