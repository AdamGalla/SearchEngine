﻿namespace Common.Shared;

public class SearchWord
{
    public List<KeyValuePair<int, int>>? DocIds { get; set; }
    public List<int>? Top10 { get; set; }
    public List<string>? Top10Details { get; set; }
    public TimeSpan Used { get; set; }
}
