using DataFormatter.FormatterLogic.Model;
using DataFormatter.StrategyFactory;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DataFormater.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormatterController : ControllerBase
{
    private IStrategyFactory _strategyFactory;
    public FormatterController(IStrategyFactory strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }

    // GET api/<Formatter>/{strategy}
    [HttpGet("{strategy}")]
    public async Task<ActionResult<string>> GetAsync([FromBody] FileData data, string strategy)
    {
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
