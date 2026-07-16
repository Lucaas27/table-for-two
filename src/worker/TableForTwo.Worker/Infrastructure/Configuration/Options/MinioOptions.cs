namespace TableForTwo.Worker.Infrastructure.Configuration.Options;

/// <summary>
/// Configuration for optional MinIO storage.
/// </summary>
public sealed class MinioOptions
{
    /// <summary>
    /// The configuration section name.
    /// </summary>
    public const string SectionName = "Infrastructure:Minio";

    /// <summary>
    /// Indicates whether MinIO integration is enabled.
    /// </summary>
    public bool Enabled { get; init; }

    /// <summary>
    /// The MinIO service endpoint.
    /// </summary>
    public string Endpoint { get; init; } = string.Empty;

    /// <summary>
    /// The MinIO access key.
    /// </summary>
    public string AccessKey { get; init; } = string.Empty;

    /// <summary>
    /// The MinIO secret key.
    /// </summary>
    public string SecretKey { get; init; } = string.Empty;

    /// <summary>
    /// The bucket used by the application.
    /// </summary>
    public string Bucket { get; init; } = string.Empty;

    /// <summary>
    /// Indicates whether SSL should be used for MinIO requests.
    /// </summary>
    public bool UseSsl { get; init; }
}
