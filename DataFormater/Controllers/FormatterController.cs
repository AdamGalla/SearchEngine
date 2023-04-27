using Common;
using DataFormatter.FormatterLogic.Model;
using DataFormatter.StrategyFactory;
using FeatureHubSDK;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Context.Propagation;
using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;

namespace DataFormater.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormatterController : ControllerBase
{
    private IStrategyFactory _strategyFactory;
    private static readonly TextMapPropagator Propagator = new TraceContextPropagator();
    public IClientContext _featureHubContext;
    public IFeatureHubConfig _featureHubConfig;

    public FormatterController(IStrategyFactory strategyFactory, IClientContext featureHubContext, IFeatureHubConfig featureHubConfig)
    {
        _strategyFactory = strategyFactory;
        _featureHubContext = featureHubContext;
        _featureHubConfig = featureHubConfig;
    }

    // GET api/<Formatter>/{strategy}
    [HttpGet("{strategy}")]
    public async Task<ActionResult<string>> GetAsync([FromBody] FileData data, string strategy)
    {
        //using var activity = Monitoring.ActivitySource.StartActivity(); handled by middleware
        //using var activity = Monitoring.ActivitySource.StartActivity();
        //Console.WriteLine(Monitoring.ActivitySource.HasListeners());
        Monitoring.Log.Information("Starting data format with strategy {Strategy}", strategy);
        var featureValue = _featureHubContext["dataformatter"];
        if (featureValue.BooleanValue == true || featureValue == null)
        {
            var parentContext = Propagator.Extract(default, Request.Headers, (headers, name) =>
            {
                if (headers.TryGetValue(name, out var value))
                {
                    return new[] { value.ToString() };
                }

                return Enumerable.Empty<string>();
            });

            using var activity = Monitoring.ActivitySource.StartActivity("Received HTTP request", ActivityKind.Consumer, parentContext.ActivityContext);

            if (data is null)
            {
                Monitoring.Log.Warning("Given data was null");
                return BadRequest("Data can not be null!");
            }

            if (!Enum.TryParse<StrategyType>(strategy, out var strategyType))
            {
                Monitoring.Log.Warning("Unknown strategy type {Strategy}", strategy);
                return BadRequest($"Unknown strategy type {strategy}! Possible values are: {Enum.GetNames(typeof(StrategyType)).Select(s => s.ToString()).ToList()}");
            }
            Monitoring.Log.Information("Using strategy: {Strategy}", strategy);

            string result = await _strategyFactory.GetStrategyType(strategyType).FormatTextAsync(data);

            Monitoring.Log.Information("Formatted data successfully: {Data}", data);
            return Ok(result);

        }
        else
        {
            Monitoring.Log.Warning("Feature is disabled and cannot be used!");
            return BadRequest("This feature is not enabled!");
        }

    }
}
