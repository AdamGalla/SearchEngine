namespace LoadBalancer.LoadBalancer.Strategies;

public class RoundRobbinStrategy : ILoadBalancerStrategy
{
    private int currentService = 0;

    public string NextService(List<string> services)
    {
        string selectedService = services[currentService];

        currentService++;
        if(currentService >= services.Count)
        {
            currentService = 0;
        }

        return selectedService;
    }
}
