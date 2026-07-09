using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TableForTwo.API.Tests;

public class ScaffoldSmokeTests
{
    [Fact]
    public void ApiAssembly_Loads()
    {
        var assembly = Assembly.Load("TableForTwo.API");

        Assert.Equal("TableForTwo.API", assembly.GetName().Name);
    }

    [Fact]
    public void WorkerAssembly_Loads()
    {
        var assembly = Assembly.Load("TableForTwo.Worker");

        Assert.Equal("TableForTwo.Worker", assembly.GetName().Name);
    }

    [Fact]
    public void WorkerHost_CanBeConstructed()
    {
        var builder = Host.CreateApplicationBuilder();
        builder.Services.AddHostedService<TableForTwo.Worker.Worker>();
        using var host = builder.Build();

        var hostedServices = host.Services.GetServices<IHostedService>();
        Assert.Contains(hostedServices, service => service is TableForTwo.Worker.Worker);
    }
}
