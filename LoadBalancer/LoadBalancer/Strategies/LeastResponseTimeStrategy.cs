using System.Runtime.CompilerServices;

namespace LoadBalancer.LoadBalancer.Strategies;

public class LeastResponseTimeStrategy : ILoadBalancerStrategy
{
    public Service NextService(List<Service> services)
    {
        return services.MinBy(service => service.AverageResponseTime);
    }
}
