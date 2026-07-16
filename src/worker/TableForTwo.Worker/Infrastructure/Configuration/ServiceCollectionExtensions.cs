using TableForTwo.Worker.Infrastructure.Configuration.Options;

namespace TableForTwo.Worker.Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<PostgresOptions>()
            .Bind(configuration.GetSection(PostgresOptions.SectionName))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.ConnectionString),
                $"{PostgresOptions.SectionName}:ConnectionString is required.")
            .ValidateOnStart();

        services
            .AddOptions<MailpitOptions>()
            .Bind(configuration.GetSection(MailpitOptions.SectionName))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.Host),
                $"{MailpitOptions.SectionName}:Host is required.")
            .Validate(
                options => options.Port is >= 1 and <= 65535,
                $"{MailpitOptions.SectionName}:Port must be between 1 and 65535.")
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.SenderEmail),
                $"{MailpitOptions.SectionName}:SenderEmail is required.")
            .ValidateOnStart();

        services
            .AddOptions<MinioOptions>()
            .Bind(configuration.GetSection(MinioOptions.SectionName))
            .Validate(
                options => !options.Enabled || IsMinioConfigurationComplete(options),
                "Infrastructure:Minio endpoint, access key, secret key, and bucket are required when enabled.")
            .ValidateOnStart();

        return services;
    }

    private static bool IsMinioConfigurationComplete(MinioOptions options)
    {
        return !string.IsNullOrWhiteSpace(options.Endpoint)
               && !string.IsNullOrWhiteSpace(options.AccessKey)
               && !string.IsNullOrWhiteSpace(options.SecretKey)
               && !string.IsNullOrWhiteSpace(options.Bucket);
    }
}