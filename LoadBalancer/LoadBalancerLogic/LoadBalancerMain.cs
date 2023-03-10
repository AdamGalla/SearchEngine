

namespace LoadBalancer.LoadBalancerLogic;

public class LoadBalancerMain : ILoadBalancer
{
    List<Service> _services = new List<Service>();
    ILoadBalancerStrategy _strategy;

    public LoadBalancerMain(ILoadBalancerStrategy strategy)
    {
        SetActiveStrategy(strategy);
    }

    public int AddService(Service service)
    {
        _services.Add(service);
        return _services.Count - 1; // return the index of the last service
    }

    public ILoadBalancerStrategy GetActiveStrategy()
    {
        return _strategy;
    }

    public List<Service> GetAllServices()
    {
        return _services;
    }

    public Service NextService()
    {
        return _strategy.NextService(_services);
    }

    public int RemoveService(string url)
    {
        return _services.Remove(_services.First(service => service.Uri.Equals(url))) ? 1 : 0;
    }

    public void SetActiveStrategy(ILoadBalancerStrategy strategy)
    {
        _strategy = strategy;
    }
}
