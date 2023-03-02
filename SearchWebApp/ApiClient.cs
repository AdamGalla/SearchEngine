using Newtonsoft.Json;
using System.Net;
using SearchWebApp.Models;

namespace SearchWebApp;

internal class ApiClient
{
    public static SearchWord GetSearchData(string input) 
    {
        using var client = new WebClient();
        client.Headers.Add("Content-Type:application/json");
        client.Headers.Add("Accept:application/json");
        var json = client.DownloadString("http://localhost:7013/api/Search/" + input);
        var searchWord = JsonConvert.DeserializeObject<SearchWord>(json);
        return searchWord;

    }
}
