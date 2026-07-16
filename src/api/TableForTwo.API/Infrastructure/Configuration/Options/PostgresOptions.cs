namespace TableForTwo.API.Infrastructure.Configuration.Options;

/// <summary>
/// Configuration for the API PostgreSQL connection.
/// </summary>
public sealed class PostgresOptions
{
    /// <summary>
    /// The configuration section name.
    /// </summary>
    public const string SectionName = "Infrastructure:Postgres";

    /// <summary>
    /// The PostgreSQL connection string.
    /// </summary>
    public string ConnectionString { get; init; } = string.Empty;
}
