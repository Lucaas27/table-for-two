namespace TableForTwo.Worker.Infrastructure.Configuration.Options;

public sealed class PostgresOptions
{
    public const string SectionName = "Infrastructure:Postgres";

    public string ConnectionString { get; init; } = string.Empty;
}