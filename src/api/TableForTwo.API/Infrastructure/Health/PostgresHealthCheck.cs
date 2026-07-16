using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Npgsql;
using TableForTwo.API.Infrastructure.Configuration.Options;

namespace TableForTwo.API.Infrastructure.Health;

public sealed class PostgresHealthCheck(IOptions<PostgresOptions> postgresOptions) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await using var connection = new NpgsqlConnection(postgresOptions.Value.ConnectionString);
            await connection.OpenAsync(cancellationToken);

            return HealthCheckResult.Healthy("PostgreSQL is reachable.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("PostgreSQL is unavailable.", ex);
        }
    }
}
