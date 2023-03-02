using Microsoft.AspNetCore.Mvc;
using SearchAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SearchAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    // GET api/<ValuesController>/5
    [HttpGet("{input}")]
    public ActionResult<SeachWord> Search(string input)
    {
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
