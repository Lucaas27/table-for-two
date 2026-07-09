namespace TableForTwo.API.Infrastructure.Configuration.Options;

public sealed class MinioOptions
{
    public const string SectionName = "Infrastructure:Minio";

    public bool Enabled { get; init; }

    public string Endpoint { get; init; } = string.Empty;

    public string AccessKey { get; init; } = string.Empty;

    public string SecretKey { get; init; } = string.Empty;

    public string Bucket { get; init; } = string.Empty;

    public bool UseSsl { get; init; }
}