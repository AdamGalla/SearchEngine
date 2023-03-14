using System.Net;
using Newtonsoft.Json;
using Common.Shared;
using RestSharp;

namespace ConsoleSearch;

internal class ApiClient
{

    public static SearchWord GetSearchData(string input)
    {
        var client = new RestClient("http://localhost:9000/LoadBalancer/");
        var request = new RestRequest("Search/" + input, Method.Get);
        var response = client.Execute<SearchWord>(request);

        return response.Data;
    }
}
