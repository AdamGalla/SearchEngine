namespace DataFormatter.FormatterLogic.Model;

public record FileData
{
    public string SearchWord { get; set; }
    public List<KeyValuePair<int, int>>? DocIds { get; set; } // the occurances of the search word
    public List<int>? Top10 { get; set; }
    public List<string>? Top10Details { get; set; } // the file paths
    public TimeSpan Used { get; set; } // the time it took to find the results
}
