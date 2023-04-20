namespace LoadBalancer.LoadBalancerLogic.Strategies;

public class LeastResponseTimeStrategy : ILoadBalancerStrategy
{
    public Service NextService(List<Service> services)
    {
        return services.MinBy(service => service.AverageResponseTime) ?? services.First();
    }
}
