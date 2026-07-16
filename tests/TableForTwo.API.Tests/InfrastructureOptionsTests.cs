using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TableForTwo.API.Infrastructure.Configuration;
using ApiMailpitOptions = TableForTwo.API.Infrastructure.Configuration.Options.MailpitOptions;
using ApiMinioOptions = TableForTwo.API.Infrastructure.Configuration.Options.MinioOptions;
using ApiPostgresOptions = TableForTwo.API.Infrastructure.Configuration.Options.PostgresOptions;
using WorkerMailpitOptions = TableForTwo.Worker.Infrastructure.Configuration.Options.MailpitOptions;
using WorkerMinioOptions = TableForTwo.Worker.Infrastructure.Configuration.Options.MinioOptions;
using WorkerPostgresOptions = TableForTwo.Worker.Infrastructure.Configuration.Options.PostgresOptions;
using WorkerServiceCollectionExtensions = TableForTwo.Worker.Infrastructure.Configuration.ServiceCollectionExtensions;

namespace TableForTwo.API.Tests;

public class InfrastructureOptionsTests
{
    [Fact]
    public void ApiOptions_BindValidConfiguration()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            [$"{ApiPostgresOptions.SectionName}:ConnectionString"] =
                "Host=localhost;Port=5432;Database=table_for_two_dev;Username=table_for_two;Password=table_for_two",
            [$"{ApiMailpitOptions.SectionName}:Host"] = "localhost",
            [$"{ApiMailpitOptions.SectionName}:Port"] = "1025",
            [$"{ApiMailpitOptions.SectionName}:SenderEmail"] = "noreply@tablefortwo.local",
            [$"{ApiMinioOptions.SectionName}:Enabled"] = "false"
        });

        using var provider = BuildServiceProvider(configuration, (services, config) => services.AddApplicationOptions(config));

        var postgresOptions = provider.GetRequiredService<IOptions<ApiPostgresOptions>>().Value;

        Assert.Contains("Database=table_for_two_dev", postgresOptions.ConnectionString);
    }

    [Fact]
    public void ApiOptions_MissingPostgresConnectionString_ThrowsValidationError()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            [$"{ApiMailpitOptions.SectionName}:Host"] = "localhost",
            [$"{ApiMailpitOptions.SectionName}:Port"] = "1025",
            [$"{ApiMailpitOptions.SectionName}:SenderEmail"] = "noreply@tablefortwo.local"
        });

        using var provider = BuildServiceProvider(configuration, (services, config) => services.AddApplicationOptions(config));

        var exception = Assert.Throws<OptionsValidationException>(
            () => provider.GetRequiredService<IOptions<ApiPostgresOptions>>().Value);

        Assert.Contains("ConnectionString is required", exception.Message);
    }

    [Fact]
    public void ApiOptions_MinioEnabledWithoutRequiredFields_ThrowsValidationError()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            [$"{ApiPostgresOptions.SectionName}:ConnectionString"] =
                "Host=localhost;Port=5432;Database=table_for_two_dev;Username=table_for_two;Password=table_for_two",
            [$"{ApiMailpitOptions.SectionName}:Host"] = "localhost",
            [$"{ApiMailpitOptions.SectionName}:Port"] = "1025",
            [$"{ApiMailpitOptions.SectionName}:SenderEmail"] = "noreply@tablefortwo.local",
            [$"{ApiMinioOptions.SectionName}:Enabled"] = "true"
        });

        using var provider = BuildServiceProvider(configuration, (services, config) => services.AddApplicationOptions(config));

        var exception = Assert.Throws<OptionsValidationException>(
            () => provider.GetRequiredService<IOptions<ApiMinioOptions>>().Value);

        Assert.Contains("required when enabled", exception.Message);
    }

    [Fact]
    public void ApiOptions_MinioEnabledWithRequiredFields_BindsSuccessfully()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            [$"{ApiPostgresOptions.SectionName}:ConnectionString"] =
                "Host=localhost;Port=5432;Database=table_for_two_dev;Username=table_for_two;Password=table_for_two",
            [$"{ApiMailpitOptions.SectionName}:Host"] = "localhost",
            [$"{ApiMailpitOptions.SectionName}:Port"] = "1025",
            [$"{ApiMailpitOptions.SectionName}:SenderEmail"] = "noreply@tablefortwo.local",
            [$"{ApiMinioOptions.SectionName}:Enabled"] = "true",
            [$"{ApiMinioOptions.SectionName}:Endpoint"] = "minio:9000",
            [$"{ApiMinioOptions.SectionName}:AccessKey"] = "minioadmin",
            [$"{ApiMinioOptions.SectionName}:SecretKey"] = "minioadmin",
            [$"{ApiMinioOptions.SectionName}:Bucket"] = "table-for-two-dev"
        });

        using var provider = BuildServiceProvider(configuration, (services, config) => services.AddApplicationOptions(config));

        var minioOptions = provider.GetRequiredService<IOptions<ApiMinioOptions>>().Value;

        Assert.True(minioOptions.Enabled);
        Assert.Equal("minio:9000", minioOptions.Endpoint);
        Assert.Equal("table-for-two-dev", minioOptions.Bucket);
    }

    [Fact]
    public void WorkerOptions_MissingPostgresConnectionString_ThrowsValidationError()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["Infrastructure:Mailpit:Host"] = "localhost",
            ["Infrastructure:Mailpit:Port"] = "1025",
            ["Infrastructure:Mailpit:SenderEmail"] = "noreply@tablefortwo.local"
        });

        using var provider = BuildServiceProvider(configuration, (services, config) => WorkerServiceCollectionExtensions.AddApplicationOptions(services, config));

        var exception = Assert.Throws<OptionsValidationException>(
            () => provider.GetRequiredService<IOptions<WorkerPostgresOptions>>().Value);

        Assert.Contains("ConnectionString is required", exception.Message);
    }

    [Fact]
    public void WorkerOptions_MailpitMissingHost_ThrowsValidationError()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["Infrastructure:Postgres:ConnectionString"] = "Host=localhost;Port=5432;Database=table_for_two_dev;Username=table_for_two;Password=table_for_two",
            ["Infrastructure:Mailpit:Host"] = string.Empty,
            ["Infrastructure:Mailpit:Port"] = "1025",
            ["Infrastructure:Mailpit:SenderEmail"] = "noreply@tablefortwo.local"
        });

        using var provider = BuildServiceProvider(configuration, (services, config) => WorkerServiceCollectionExtensions.AddApplicationOptions(services, config));

        var exception = Assert.Throws<OptionsValidationException>(
            () => provider.GetRequiredService<IOptions<WorkerMailpitOptions>>().Value);

        Assert.Contains("Host is required", exception.Message);
    }

    [Fact]
    public void WorkerOptions_MinioEnabledWithRequiredFields_BindsSuccessfully()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["Infrastructure:Postgres:ConnectionString"] = "Host=localhost;Port=5432;Database=table_for_two_dev;Username=table_for_two;Password=table_for_two",
            ["Infrastructure:Mailpit:Host"] = "localhost",
            ["Infrastructure:Mailpit:Port"] = "1025",
            ["Infrastructure:Mailpit:SenderEmail"] = "noreply@tablefortwo.local",
            ["Infrastructure:Minio:Enabled"] = "true",
            ["Infrastructure:Minio:Endpoint"] = "minio:9000",
            ["Infrastructure:Minio:AccessKey"] = "minioadmin",
            ["Infrastructure:Minio:SecretKey"] = "minioadmin",
            ["Infrastructure:Minio:Bucket"] = "table-for-two-dev"
        });

        using var provider = BuildServiceProvider(configuration, (services, config) => WorkerServiceCollectionExtensions.AddApplicationOptions(services, config));

        var minioOptions = provider.GetRequiredService<IOptions<WorkerMinioOptions>>().Value;

        Assert.True(minioOptions.Enabled);
        Assert.Equal("minio:9000", minioOptions.Endpoint);
        Assert.Equal("table-for-two-dev", minioOptions.Bucket);
    }

    private static IConfiguration BuildConfiguration(Dictionary<string, string?> settings)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();
    }

    private static ServiceProvider BuildServiceProvider(
        IConfiguration configuration,
        Action<IServiceCollection, IConfiguration> addApplicationOptions)
    {
        var services = new ServiceCollection();
        addApplicationOptions(services, configuration);
        return services.BuildServiceProvider();
    }
}
