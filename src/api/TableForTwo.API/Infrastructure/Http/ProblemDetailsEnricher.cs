using Microsoft.AspNetCore.Mvc;

namespace TableForTwo.API.Infrastructure.Http;

public static class ProblemDetailsEnricher
{
    public static void Enrich(HttpContext httpContext, ProblemDetails problemDetails)
    {
        problemDetails.Extensions["requestId"] = httpContext.TraceIdentifier;
        problemDetails.Extensions["correlationId"] = RequestCorrelationMiddleware.GetCorrelationId(httpContext);
    }
}
