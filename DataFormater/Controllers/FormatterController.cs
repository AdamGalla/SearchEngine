using DataFormatter.FormatterLogic.Model;
using Microsoft.AspNetCore.Mvc;

namespace DataFormater.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormatterController : ControllerBase
{
    // GET api/<FormatterController>
    [HttpGet]
    public ActionResult<string> Get([FromBody] FileData data)
    {
        if (data is null)
        {
            return BadRequest("Data can not be null!");
        }

        string result = "";

        return Ok(result);
    }
}
