using LoadBalancer.LoadBalancerLogic;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoadBalancer.Controllers;
[Route("[controller]")]
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

    [HttpGet("search/{input}")]
    public IActionResult Search(string input)
    {
        Console.WriteLine($"Started searching for Word: {input}");
        try 
        {
            var timer = Stopwatch.StartNew();
            RestResponse queryResult;
            do
            {
                timer.Restart();
                var currentService = _loadBalancer.NextService();
                Console.WriteLine(currentService.Uri);
                var client = new RestClient();
                var request = new RestRequest($"http://{currentService.Uri}/Search/{input}", Method.Get);

                queryResult = client.Execute(request);
                Console.WriteLine(queryResult.IsSuccessStatusCode);
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
                        Console.WriteLine($"Removed serice {currentService.Uri} due to status code {queryResult.StatusCode}");
                        _loadBalancer.RemoveService(currentService.Uri);
                    }
                }
            }
            while (_loadBalancer.GetAllServices().Count != 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost("RegisterService")]
    public IActionResult RegisterService([FromBody] dynamic Url)
    {
        string url = Url.GetProperty("Url").GetString();
        //string url = Request.Host.ToUriComponent().ToString();
        Console.WriteLine($"Adding Service to pool {url} | {url}");
        //_logger.LogInformation("Adding Service to pool {ServiceName} | {ServiceURI}", Url, Url);
        int returnedId = _loadBalancer.AddService(new Service() { Name = url, Uri = url });
        //_logger.LogInformation("Registered Service Id: {Id}", returnedId);
        Console.WriteLine($"Registered Service Id: {returnedId}");
        return Ok();
    }
}
