using LoadBalancer.LoadBalancer;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace LoadBalancer.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoadBalancerController : ControllerBase
{
    private ILoadBalancer _loadBalancer;

    public LoadBalancerController(ILoadBalancer loadBalancer)
    {
        _loadBalancer = loadBalancer;
    }

    [HttpGet]
    public IActionResult Search(string input)
    {

        RestResponse queryResult;
        do
        {
            string url = _loadBalancer.NextService();

            var client = new RestClient();
            var request = new RestRequest($"{url}/{input}", Method.Get);

            queryResult = client.Execute(request);

            if (queryResult.IsSuccessStatusCode)
            {
                return Ok(queryResult.Content);
            }
            else
            {
                _loadBalancer.RemoveService(url);
            }
        }
        while(_loadBalancer.GetAllServices().Count != 0);

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}
