using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using RestSharp;
using System.Diagnostics;

namespace Common;
public static class RestSharpExtentionMethods
{
    private static readonly TextMapPropagator Propagator = new TraceContextPropagator();

    public static async Task<RestResponse<T>> ExecuteWithTracingAsync<T>(this IRestClient client, RestRequest request) //where T : new()
    {
        using var activity = Monitoring.ActivitySource.StartActivity(ActivityKind.Client);
        var activityContext = activity?.Context ?? Activity.Current?.Context ?? default;

        var propagationContext = new PropagationContext(activityContext, Baggage.Current);
        Propagators.DefaultTextMapPropagator.Inject(propagationContext, request, (restRequest, key, value) =>
        {
            restRequest.AddHeader(key, value);
        });

        return await client.ExecuteAsync<T>(request);
    }
}
