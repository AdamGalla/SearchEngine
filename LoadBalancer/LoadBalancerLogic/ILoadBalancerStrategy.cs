namespace LoadBalancer.LoadBalancerLogic;

public interface ILoadBalancerStrategy
{
    public Service NextService(List<Service> services);
}
