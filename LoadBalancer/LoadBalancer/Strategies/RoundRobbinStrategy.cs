namespace LoadBalancer.LoadBalancer.Strategies;

public class RoundRobbinStrategy : ILoadBalancerStrategy
{
    private int currentService = 0;

    public Service NextService(List<Service> services)
    {
        Service selectedService = services[currentService];

        currentService++;
        if(currentService >= services.Count)
        {
            currentService = 0;
        }

        return selectedService;
    }
}
