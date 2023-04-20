using Common.Shared;
using Microsoft.AspNetCore.Mvc;
using SearchWebApp.Models;
using System.Diagnostics;
using System.Xml.Linq;

namespace SearchWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IApiClient _apiClient;
    public HomeController(ILogger<HomeController> logger, IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<ActionResult> Search(string input)
    {
        var searchWord = await _apiClient.GetSearchData(input);
        ViewBag.prevInput = input;
        return View("Index", searchWord);
    }
    [HttpGet]
    public async Task<ActionResult> FormatData(string formatType)
    {

        var formatResult = _apiClient.GetFormattedData(formatType);
        string xmlString = formatResult;
        XDocument xmlDoc = XDocument.Parse(xmlString);
        ViewBag.FormattedData = xmlDoc;
        ViewBag.FormatType = formatType;
        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}