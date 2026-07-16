using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using TableForTwo.API.Tests.Infrastructure;

namespace TableForTwo.API.Tests;

[Collection(IntegrationCollection.Name)]
public sealed class HealthAndErrorContractTests(PostgresContainerFixture postgres) : IAsyncLifetime
{
    private TestApiFactory? _factory;
    private HttpClient? _client;

    public Task InitializeAsync()
    {
        _factory = new TestApiFactory(postgres);
        _client = _factory.CreateClient();
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        if (_client is not null)
        {
            _client.Dispose();
        }

        if (_factory is not null)
        {
            await _factory.DisposeAsync();
        }
    }

    [Fact]
    public async Task HealthEndpoint_ReportsApiAndDatabaseReadiness()
    {
        var response = await Client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        using var payload = await ReadJsonAsync(response);
        Assert.Equal("Healthy", payload.RootElement.GetProperty("status").GetString());
        Assert.Equal("Healthy", payload.RootElement.GetProperty("checks").GetProperty("api").GetProperty("status").GetString());
        Assert.Equal("Healthy", payload.RootElement.GetProperty("checks").GetProperty("postgres").GetProperty("status").GetString());
    }

    [Fact]
    public async Task ErrorResponses_UseConsistentProblemDetailsEnvelope()
    {
        using var validationResponse = await Client.PostAsJsonAsync("/WeatherForecast/validate", new { servings = 0 });
        using var notFoundResponse = await Client.GetAsync("/WeatherForecast/missing");
        using var conflictResponse = await Client.PostAsync("/WeatherForecast/conflict", content: null);
        using var serverErrorResponse = await Client.GetAsync("/WeatherForecast/fail");

        await AssertProblemDetailsAsync(validationResponse, HttpStatusCode.BadRequest, expectedErrors: true);
        await AssertProblemDetailsAsync(notFoundResponse, HttpStatusCode.NotFound);
        await AssertProblemDetailsAsync(conflictResponse, HttpStatusCode.Conflict);
        await AssertProblemDetailsAsync(serverErrorResponse, HttpStatusCode.InternalServerError);
    }

    private HttpClient Client => _client ?? throw new InvalidOperationException("Test client has not been initialised.");

    private static async Task AssertProblemDetailsAsync(HttpResponseMessage response, HttpStatusCode expectedStatusCode, bool expectedErrors = false)
    {
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
        Assert.True(response.Headers.Contains("X-Correlation-ID"));

        using var payload = await ReadJsonAsync(response);
        var root = payload.RootElement;

        Assert.Equal((int)expectedStatusCode, root.GetProperty("status").GetInt32());
        Assert.True(root.TryGetProperty("requestId", out var requestId));
        Assert.False(string.IsNullOrWhiteSpace(requestId.GetString()));
        Assert.True(root.TryGetProperty("correlationId", out var correlationId));
        Assert.False(string.IsNullOrWhiteSpace(correlationId.GetString()));

        if (expectedErrors)
        {
            Assert.True(root.TryGetProperty("errors", out var errors));
            Assert.True(errors.EnumerateObject().Any());
        }
    }

    private static async Task<JsonDocument> ReadJsonAsync(HttpResponseMessage response)
    {
        return JsonDocument.Parse(await response.Content.ReadAsStringAsync());
    }
}
