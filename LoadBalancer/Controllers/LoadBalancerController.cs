using LoadBalancer.LoadBalancer;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;

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
        var timer = Stopwatch.StartNew();
        RestResponse queryResult;
        do
        {
            timer.Restart();
            var currentService = _loadBalancer.NextService();

            var client = new RestClient();
            var request = new RestRequest($"{currentService.Uri}/{input}", Method.Get);

            queryResult = client.Execute(request);

            if (queryResult.IsSuccessStatusCode)
            {
                timer.Stop();
                currentService.AddLatestResponseTime(timer.ElapsedMilliseconds);
                return Ok(queryResult.Content);
            }
            else
            {
                if (((int)queryResult.StatusCode) > 500)
                {
                    _loadBalancer.RemoveService(currentService.Uri);
                }
            }
        }
        while(_loadBalancer.GetAllServices().Count != 0);

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost]
    public IActionResult RegisterService([FromRoute] string serviceUrl) 
    {
        string url = Request.Host.ToUriComponent().ToString();
        _logger.LogInformation("Adding Service to pool {ServiceName} | {ServiceURI}", serviceUrl, serviceUrl);
        int returnedId = _loadBalancer.AddService(new Service() { Name = serviceUrl, Uri = url });
        _logger.LogInformation("Registered Service Id: {Id}", returnedId);
        return Ok();
    }
}
