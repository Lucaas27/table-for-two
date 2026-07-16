using System.Diagnostics;

namespace TableForTwo.API.Infrastructure.Http;

/// <summary>
/// Adds a request correlation identifier to log scopes and response headers.
/// </summary>
public sealed class RequestCorrelationMiddleware(RequestDelegate next, ILogger<RequestCorrelationMiddleware> logger)
{
    /// <summary>
    /// The header used to propagate correlation identifiers.
    /// </summary>
    public const string HeaderName = "X-Correlation-ID";
    private const int MaxCorrelationIdLength = 128;

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = GetIncomingCorrelationId(context) ?? context.TraceIdentifier;
        context.Response.OnStarting(() =>
        {
            context.Response.Headers[HeaderName] = correlationId;
            return Task.CompletedTask;
        });

        using (logger.BeginScope(new Dictionary<string, object?>
               {
                   ["RequestId"] = context.TraceIdentifier,
                   ["CorrelationId"] = correlationId
               }))
        {
            var stopwatch = Stopwatch.StartNew();

            await next(context);

            logger.LogInformation(
                "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMs}ms.",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }

    /// <summary>
    /// Resolves the correlation identifier for the current request.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>The correlation identifier that should be used for this request.</returns>
    public static string GetCorrelationId(HttpContext context)
    {
        if (context.Response.Headers.TryGetValue(HeaderName, out var responseValue)
            && !string.IsNullOrWhiteSpace(responseValue.ToString()))
        {
            return responseValue.ToString();
        }

        return GetIncomingCorrelationId(context) ?? context.TraceIdentifier;
    }

    private static string? GetIncomingCorrelationId(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue(HeaderName, out var requestValue)
            && !string.IsNullOrWhiteSpace(requestValue.ToString()))
        {
            var correlationId = requestValue.ToString();
            return correlationId.Length <= MaxCorrelationIdLength
                ? correlationId
                : correlationId[..MaxCorrelationIdLength];
        }

        return null;
    }
}
