namespace LoadBalancer.LoadBalancerLogic.Strategies;

public class RoundRobbinStrategy : ILoadBalancerStrategy
{
    private int _currentService = 0;

    public Service NextService(List<Service> services)
    {
        if (services.Count > 0 && _currentService < services.Count) 
        {
            Service selectedService = services[_currentService];

            _currentService++;
            if (_currentService >= services.Count)
            {
                _currentService = 0;
            }
            return selectedService;
        }
        throw new IndexOutOfRangeException("No services are registered in the load balancer!");
    }
}
