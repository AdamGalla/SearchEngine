using RestSharp;

namespace SearchAPI;

public class RegisterService
{
    public static void Register(string loadBalancerUrl, string serviceUrl)
    {
        var client = new RestClient(loadBalancerUrl);
        var request = new RestRequest($"RegisterService/{serviceUrl}", Method.Post);
        var queryResult = client.Execute(request);
        //TODO: Log Result
    }
}
