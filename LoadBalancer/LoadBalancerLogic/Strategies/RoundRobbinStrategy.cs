namespace LoadBalancer.LoadBalancerLogic.Strategies;

public class RoundRobbinStrategy : ILoadBalancerStrategy
{
    private int currentService = 0;

    public Service NextService(List<Service> services)
    {
        if (currentService >= 0 && currentService < services.Count) 
        {
            Service selectedService = services[currentService];

            currentService++;
            if (currentService >= services.Count)
            {
                currentService = 0;
            }

            return selectedService;
        } 
        else 
        { 
            return new Service();
        }
        
    }
}
