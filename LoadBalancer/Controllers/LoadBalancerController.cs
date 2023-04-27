using Common.Shared;
using LoadBalancer.LoadBalancerLogic;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;

namespace LoadBalancer.Controllers;
[Route("[controller]")]
[ApiController]
public class LoadBalancerController : ControllerBase
{
    private readonly ILoadBalancer _loadBalancer;

    public LoadBalancerController(ILoadBalancer loadBalancer)
    {
        _loadBalancer = loadBalancer;
    }

    [HttpGet("search/{input}")]
    public ActionResult<SearchWord> Search(string input)
    {
        Console.WiteLine($"Started searching for Word: {input}");
        try
        {
            var timer = Stopwatch.StartNew();
            RestResponse<SearchWord> queryResult;
            do
            {
                timer.Restart();
                var currentService = _loadBalancer.NextService();
                Console.WriteLine(currentService.Uri);
                var client = new RestClient();
                var request = new RestRequest($"http://{currentService.Uri}/Search/{input}", Method.Get);

                queryResult = client.Execute<SearchWord>(request);
                //Console.WriteLine(queryResult.IsSuccessStatusCode);
                
                var services = _loadBalancer.GetAllServices();
                foreach (var service in services) 
                {
                    
                    Console.WriteLine("Service name:" + service.Uri);
                    Console.WriteLine("Service response time:" + service.AverageResponseTime);
                }
                
                if (queryResult.IsSuccessStatusCode)
                {
                    timer.Stop();
                    currentService.AddLatestResponseTime(timer.ElapsedMilliseconds);
                    return Ok(queryResult.Data);
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
        Console.WriteLine($"Adding Service to pool {url} | {url}");

        int returnedId = _loadBalancer.AddService(new Service() { Name = url, Uri = url });
        Console.WriteLine($"Registered Service Id: {returnedId}");
        return Ok();
    }
}
