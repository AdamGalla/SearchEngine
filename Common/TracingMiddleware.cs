using Microsoft.AspNetCore.Http;
using OpenTelemetry.Context.Propagation;
using System.Diagnostics;

namespace Common;
public class TracingMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly TextMapPropagator Propagator = new TraceContextPropagator();

    public TracingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var parentContext = Propagator.Extract(default, context.Request.Headers, (headers, name) =>
        {
            if (headers.TryGetValue(name, out var value))
            {
                return new[] { value.ToString() };
            }

            return Enumerable.Empty<string>();
        });

        using var activity = Monitoring.ActivitySource.StartActivity("Received HTTP request", ActivityKind.Consumer, parentContext.ActivityContext);

        await _next(context);
    }
}
