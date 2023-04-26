using Common;
using DataFormatter.FormatterLogic.Model;
using DataFormatter.StrategyFactory;
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

    public FormatterController(IStrategyFactory strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }

    // GET api/<Formatter>/{strategy}
    [HttpGet("{strategy}")]
    public async Task<ActionResult<string>> GetAsync([FromBody] FileData data, string strategy)
    {
        //using var activity = Monitoring.ActivitySource.StartActivity(); handled by middleware
        //using var activity = Monitoring.ActivitySource.StartActivity();
        //Console.WriteLine(Monitoring.ActivitySource.HasListeners());
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
            return BadRequest("Data can not be null!");
        }

        if (!Enum.TryParse<StrategyType>(strategy, out var strategyType))
        {
            return BadRequest($"Unknown strategy type {strategy}! Possible values are: {Enum.GetNames(typeof(StrategyType)).Select(s => s.ToString()).ToList()}");
        }

        string result = await _strategyFactory.GetStrategyType(strategyType).FormatTextAsync(data);

        return Ok(result);
    }
}
