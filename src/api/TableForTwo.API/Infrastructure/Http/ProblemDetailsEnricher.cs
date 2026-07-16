using Microsoft.AspNetCore.Mvc;

namespace TableForTwo.API.Infrastructure.Http;

/// <summary>
/// Adds request metadata to problem details responses.
/// </summary>
public static class ProblemDetailsEnricher
{
    /// <summary>
    /// Adds request and correlation identifiers to a problem details payload.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="problemDetails">The problem details instance to enrich.</param>
    public static void Enrich(HttpContext httpContext, ProblemDetails problemDetails)
    {
        problemDetails.Extensions["requestId"] = httpContext.TraceIdentifier;
        problemDetails.Extensions["correlationId"] = RequestCorrelationMiddleware.GetCorrelationId(httpContext);
    }
}
