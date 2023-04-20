using Newtonsoft.Json;
using System.Net;
using SearchWebApp.Models;
using RestSharp;
using Common.Shared;
using Common;

namespace SearchWebApp;

public class ApiClient : IApiClient
{
    public SearchWordDTO _searchResult;
    
    public async Task<SearchWord> GetSearchData(string input)
    {
        var client = new RestClient("http://localhost:9000/LoadBalancer");
        var request = new RestRequest("search/{input}", Method.Get);
        request.AddParameter("input", input, ParameterType.UrlSegment);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        var response = await client.ExecuteWithTracingAsync<SearchWord>(request);

        _searchResult = new SearchWordDTO() 
        { 
        SearchWord = input,
        DocIds = response.Data.DocIds,
        Top10 = response.Data.Top10,
        Top10Details = response.Data.Top10Details,
        Used = response.Data.Used,
        };
        
        return response.Data;
    }

    public async Task<string> GetFormattedData(string formatType)
    {
        var client = new RestClient("http://localhost:9001/api/Formatter/");
        var request = new RestRequest("{strategy}", Method.Get);
        request.AddParameter("strategy", formatType, ParameterType.UrlSegment);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        request.AddJsonBody(_searchResult);
        var response = await client.ExecuteWithTracingAsync<string>(request);
        return response.Data;
    }
}
