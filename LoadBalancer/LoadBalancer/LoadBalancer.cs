namespace LoadBalancer.LoadBalancer;

public class LoadBalancer : ILoadBalancer
{
    List<string> _services = new List<string>();
    ILoadBalancerStrategy _strategy;

    public LoadBalancer(ILoadBalancerStrategy strategy)
    {
        SetActiveStrategy(strategy);
    }

    public int AddService(string url)
    {
        _services.Add(url);
        return _services.Count - 1; // return the index of the last service
    }

    public ILoadBalancerStrategy GetActiveStrategy()
    {
        return _strategy;
    }

    public List<string> GetAllServices()
    {
        return _services;
    }

    public string NextService()
    {
        return _strategy.NextService(_services);
    }

    public int RemoveService(string url)
    {
        return _services.Remove(url) ? 1 : 0;
    }

    public void SetActiveStrategy(ILoadBalancerStrategy strategy)
    {
        _strategy = strategy;
    }
}
