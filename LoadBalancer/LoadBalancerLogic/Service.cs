namespace LoadBalancer.LoadBalancerLogic;

public record Service
{
    public string Name { get; set; }
    public string Uri { get; set; }

    private readonly int _maxTimeCount = 10;
    private readonly Queue<long> _times = new Queue<long>();

    public long AverageResponseTime => _times.Count > 0 ? (long)_times.Average() : 0;

    public void AddLatestResponseTime(long timeInMs)
    {
        if (_times.Count >= _maxTimeCount)
        {
            _times.Dequeue();
        }
        _times.Enqueue(timeInMs);
    }
}
