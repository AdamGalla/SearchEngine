using Microsoft.AspNetCore.Mvc;
using SearchAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SearchAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
   
    private readonly IConfiguration _configuration;
    public SearchController(IConfiguration configuration)
    {
        _configuration = configuration;
        //// Register self in the loadbalancer
        //string? loadBalancerURL = configuration.GetSection("LoadBalancerUrl").Value;
        //if (loadBalancerURL is null)
        //{
        //    _logger.LogWarning("Failed to retrieve loadBalancer URL. Is it defined in appsettings.json?");
        //}
        //else
        //{
        //    _logger.LogInformation("Registering self to loadbalancer... {LoadBalancerUrl} -> {Name}", loadBalancerURL, Environment.MachineName);
        //    var returnedStatusCode = RegisterService.Register(loadBalancerURL, $"http://{Environment.MachineName}");
        //    _logger.LogInformation("Registration result: {StatusCode}", ((int)returnedStatusCode));
        //}
    }

    // GET api/<ValuesController>/5
    [HttpGet("{input}")]
    public ActionResult<SeachWord> Search(string input)
    {
        Console.Write($"Searching for: {input}");
        SearchLogic mSearchLogic = new SearchLogic(new Database());
        var wordIds = new List<int>();
        var searchTerms = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        foreach (var t in searchTerms)
        {
            int id = mSearchLogic.GetIdOf(t);
            if (id != -1)
            {
                wordIds.Add(id);
            }
            //else
            //{
            //    Console.WriteLine(t + " will be ignored");
            //}
        }

        DateTime start = DateTime.Now;

        var docIds = mSearchLogic.GetDocuments(wordIds);

        // get details for the first 10             
        var top10 = new List<int>();
        foreach (var p in docIds.GetRange(0, Math.Min(10, docIds.Count)))
        {
            top10.Add(p.Key);
        }

        TimeSpan used = DateTime.Now - start;
       
        List<string>? top10details = mSearchLogic.GetDocumentDetails(top10);
        var seachWord = new SeachWord()
        {
            DocIds = docIds,
            Top10 = top10,
            Top10Details= top10details,
            Used= used
        };
        
        return seachWord;
    }

}
