using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TableForTwo.API.Infrastructure.Configuration;
using ApiMailpitOptions = TableForTwo.API.Infrastructure.Configuration.Options.MailpitOptions;
using ApiMinioOptions = TableForTwo.API.Infrastructure.Configuration.Options.MinioOptions;
using ApiPostgresOptions = TableForTwo.API.Infrastructure.Configuration.Options.PostgresOptions;
using WorkerPostgresOptions = TableForTwo.Worker.Infrastructure.Configuration.Options.PostgresOptions;

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

        var services = new ServiceCollection();
        services.AddApplicationOptions(configuration);
        using var provider = services.BuildServiceProvider();

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

        var services = new ServiceCollection();
        services.AddApplicationOptions(configuration);
        using var provider = services.BuildServiceProvider();

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

        var services = new ServiceCollection();
        services.AddApplicationOptions(configuration);
        using var provider = services.BuildServiceProvider();

        var exception = Assert.Throws<OptionsValidationException>(
            () => provider.GetRequiredService<IOptions<ApiMinioOptions>>().Value);

        Assert.Contains("required when enabled", exception.Message);
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

        var services = new ServiceCollection();
        Worker.Infrastructure.Configuration.ServiceCollectionExtensions.AddApplicationOptions(services, configuration);
        using var provider = services.BuildServiceProvider();

        var exception = Assert.Throws<OptionsValidationException>(
            () => provider.GetRequiredService<IOptions<WorkerPostgresOptions>>().Value);

        Assert.Contains("ConnectionString is required", exception.Message);
    }

    private static IConfiguration BuildConfiguration(Dictionary<string, string?> settings)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();
    }
}
