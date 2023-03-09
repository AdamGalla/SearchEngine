using System.Diagnostics;

namespace LoadBalancer.LoadBalancer;

public record Service
{
    public string Name { get; set; }
    public string Uri { get; set; }
    public long AverageResponseTime => times.Sum() / times.Count();
    private int ct = 0;
    private long[] times = new long[10];
    
    public void AddLatestResponseTime(long timeInMs) 
    {
        times[ct] = timeInMs;
        ct = (ct + 1) % times.Length; // Wrap back around to 0 when we reach the end.
    }
}
