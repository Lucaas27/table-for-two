using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TableForTwo.API.Tests.Infrastructure;

public sealed class TestApiFactory(PostgresContainerFixture postgres) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureAppConfiguration((_, configurationBuilder) =>
        {
            configurationBuilder.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Infrastructure:Postgres:ConnectionString"] = postgres.ConnectionString,
                ["Infrastructure:Mailpit:Host"] = "localhost",
                ["Infrastructure:Mailpit:Port"] = "1025",
                ["Infrastructure:Mailpit:SenderEmail"] = "noreply@tablefortwo.local",
                ["Infrastructure:Minio:Enabled"] = "false"
            });
        });
    }
}
