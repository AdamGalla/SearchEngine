namespace LoadBalancer.LoadBalancerLogic;

public interface ILoadBalancer
{
    public List<Service> GetAllServices();
    public int AddService(Service service);
    public int RemoveService(string url);
    public ILoadBalancerStrategy GetActiveStrategy();
    public void SetActiveStrategy(ILoadBalancerStrategy strategy);
    public Service NextService();
}
