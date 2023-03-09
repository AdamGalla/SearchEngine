using LoadBalancer.LoadBalancer;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace LoadBalancer.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoadBalancerController : ControllerBase
{
    private readonly ILoadBalancer _loadBalancer;
    private readonly ILogger<LoadBalancerController> _logger;

    public LoadBalancerController(ILoadBalancer loadBalancer, ILogger<LoadBalancerController> logger)
    {
        _loadBalancer = loadBalancer;
        _logger = logger;
        _logger.LogInformation("LoadBalancer started...");
    }

    [HttpGet]
    public IActionResult Search(string input)
    {

        RestResponse queryResult;
        do
        {
            string url = _loadBalancer.NextService().Uri;

            var client = new RestClient();
            var request = new RestRequest($"{url}/{input}", Method.Get);

            queryResult = client.Execute(request);

            if (queryResult.IsSuccessStatusCode)
            {
                return Ok(queryResult.Content);
            }
            else
            {
                if (((int)queryResult.StatusCode) > 500)
                {
                    _loadBalancer.RemoveService(url);
                }
            }
        }
        while(_loadBalancer.GetAllServices().Count != 0);

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost]
    public IActionResult RegisterService(string serviceName) 
    {
        string url = Request.Host.ToUriComponent().ToString();
        _logger.LogInformation("Adding Service to pool {ServiceName} | {ServiceURI}", serviceName, url);
        int returnedId = _loadBalancer.AddService(new Service() { Name = serviceName, Uri = url });
        _logger.LogInformation("Registered Service Id: {Id}", returnedId);
        return Ok();
    }
}
