using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Npgsql;
using TableForTwo.API.Infrastructure.Configuration.Options;

namespace TableForTwo.API.Infrastructure.Health;

/// <summary>
/// Verifies that PostgreSQL is reachable for health reporting.
/// </summary>
public sealed class PostgresHealthCheck(IOptions<PostgresOptions> postgresOptions) : IHealthCheck
{
    /// <summary>
    /// Executes the PostgreSQL readiness check.
    /// </summary>
    /// <param name="context">The health check execution context.</param>
    /// <param name="cancellationToken">A token that cancels the check.</param>
    /// <returns>The health status for PostgreSQL connectivity.</returns>
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
