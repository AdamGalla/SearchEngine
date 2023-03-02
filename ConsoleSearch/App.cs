using ConsoleSearch.Models;
using System;

namespace ConsoleSearch;

public class App
{
    public void Run()
    {
        
        Console.WriteLine("Console Search");
        
        while (true)
        {
            Console.WriteLine("enter search terms - q for quit");
            string input = Console.ReadLine() ?? string.Empty;
            if (input.Equals("q")) break;
            var searchWord = new SearchWord();
            searchWord = ApiClient.GetSearchData(input);
            
            int idx = 0;

            foreach (var doc in searchWord.Top10Details)
            {
                Console.WriteLine("" + (idx + 1) + ": " + doc + " -- contains " + searchWord.DocIds[idx].Value + " search terms");
                idx++;
            }
            Console.WriteLine("Documents: " + searchWord.DocIds.Count + ". Time: " + searchWord.Used);
        }
    }
}
