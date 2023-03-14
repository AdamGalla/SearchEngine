using Newtonsoft.Json;
using System.Net;
using SearchWebApp.Models;
using RestSharp;
using Common.Shared;

namespace SearchWebApp;

internal class ApiClient
{
    public static SearchWord GetSearchData(string input)
    {
        var client = new RestClient("http://localhost:9000/LoadBalancer");
        var request = new RestRequest("search/{input}", Method.Get);
        request.AddParameter("input", input, ParameterType.UrlSegment);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        var response = client.Execute<SearchWord>(request);
        return response.Data;
    }
}
