namespace LoadBalancer.LoadBalancer;

public record Service
{
    public string Name { get; set; }
    public string Uri { get; set; }
}
