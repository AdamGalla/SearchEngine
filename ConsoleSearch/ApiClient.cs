using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;
using ConsoleSearch.Models;
namespace ConsoleSearch
{
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
}
