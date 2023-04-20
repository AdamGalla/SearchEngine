using DataFormatter.FormatterLogic.Model;
using DataFormatter.StrategyFactory;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult<string> Get([FromBody] FileData data, string strategy)
    {
        if (data is null)
        {
            return BadRequest("Data can not be null!");
        }

        if (!Enum.TryParse<StrategyType>(strategy, out var strategyType))
        {
            return BadRequest($"Unknown strategy type {strategy}!");
        }

        string result = _strategyFactory.GetStrategyType(strategyType).FormatText(data);
        return Ok(result);
    }
}
