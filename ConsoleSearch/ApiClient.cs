using System.Net;
using Newtonsoft.Json;
using ConsoleSearch.Models;
using RestSharp;

namespace ConsoleSearch;

internal class ApiClient
{
    //public static SearchWord GetSearchData(string input) 
    //{
    //    using var client = new WebClient();
    //    client.Headers.Add("Content-Type:application/json");
    //    client.Headers.Add("Accept:application/json");
    //    var json = client.DownloadString("http://loadbalancer/Search/" + input);
    //    var searchWord = JsonConvert.DeserializeObject<SearchWord>(json);

    //    return searchWord;

    //}

    public static SearchWord GetSearchData(string input)
    {
        var client = new RestClient("http://localhost:9000");
        var request = new RestRequest("Search/" + input, Method.Get);
        var response = client.Execute(request);
        var json = response.Content;
        var searchWord = JsonConvert.DeserializeObject<SearchWord>(json);

        return searchWord;
    }
}
