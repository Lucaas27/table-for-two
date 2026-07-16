using Microsoft.Extensions.Options;
using Npgsql;
using TableForTwo.API.Infrastructure.Configuration.Options;

namespace TableForTwo.API.Infrastructure.Startup;

/// <summary>
/// Verifies database connectivity when the API starts.
/// </summary>
public sealed class DatabaseConnectivityStartupCheck(
    IOptions<PostgresOptions> postgresOptions,
    ILogger<DatabaseConnectivityStartupCheck> logger) : IHostedService
{
    /// <summary>
    /// Runs the startup connectivity check.
    /// </summary>
    /// <param name="cancellationToken">A token that cancels startup.</param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Checking PostgreSQL connectivity during startup.");

        const int maxAttempts = 4;
        var delay = TimeSpan.FromSeconds(1);

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                await using var connection = new NpgsqlConnection(postgresOptions.Value.ConnectionString);
                await connection.OpenAsync(cancellationToken);

                logger.LogInformation("PostgreSQL connectivity verified during startup.");
                return;
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex) when (attempt < maxAttempts)
            {
                logger.LogWarning(
                    ex,
                    "PostgreSQL connectivity check attempt {Attempt} of {MaxAttempts} failed. Retrying in {DelayMs}ms.",
                    attempt,
                    maxAttempts,
                    delay.TotalMilliseconds);

                if (delay > TimeSpan.Zero)
                {
                    await Task.Delay(delay, cancellationToken);
                }

                delay = TimeSpan.FromMilliseconds(delay.TotalMilliseconds * 2);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PostgreSQL connectivity check failed during startup after {Attempt} attempts.", attempt);
                throw;
            }
        }
    }

    /// <summary>
    /// Stops the startup check service.
    /// </summary>
    /// <param name="cancellationToken">A token that cancels shutdown.</param>
    /// <returns>A completed task.</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
