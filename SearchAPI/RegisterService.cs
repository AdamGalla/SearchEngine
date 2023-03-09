using RestSharp;
using System.Net;

namespace SearchAPI;

public class RegisterService
{
    public static HttpStatusCode Register(string loadBalancerUrl, string serviceName)
    {
        var client = new RestClient(loadBalancerUrl);
        var request = new RestRequest($"RegisterService/{serviceName}", Method.Post);
        var queryResult = client.Execute(request);
        return queryResult.StatusCode;
    }
}
