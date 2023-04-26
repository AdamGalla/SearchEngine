using Microsoft.AspNetCore.Mvc;
using SearchWebApp.Models;
using System.Diagnostics;
using System.Xml.Linq;

using Common;
using FeatureHubSDK;

namespace SearchWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IApiClient _apiClient;
    public IClientContext _featureHubContext;

    public static string? _formattedData;
    public static string? _formatType;

    public HomeController(ILogger<HomeController> logger, IApiClient apiClient, IClientContext featureHubContext)
    {
        _logger = logger;
        _apiClient = apiClient;
        _featureHubContext = featureHubContext;
    }

    public async Task<IActionResult> Index()
    {
        var featureValue = _featureHubContext["dataformatter"];
        ViewBag.FeatureValue = featureValue.Value;
        return View();
    }

    [HttpGet]
    public async Task<ActionResult> Search(string input)
    {
        var searchWord = await _apiClient.GetSearchData(input);
        var featureValue = _featureHubContext["dataformatter"];
        ViewBag.FeatureValue = featureValue.Value;
        ViewBag.prevInput = input;
        return View("Index", searchWord);
    }
    [HttpGet]
    public async Task<ActionResult> FormatData(string formatType)
    {
        var formatResult = await _apiClient.GetFormattedData(formatType);
        if(formatType == "XMLFormatter")
        {
            string xmlString = formatResult.ToString();
            XDocument xmlDoc = XDocument.Parse(xmlString);
            ViewBag.FormattedData = xmlDoc;
            _formattedData = xmlString;
        }
        else
        {
            ViewBag.FormattedData = formatResult;
            _formattedData = formatResult;
        }
        ViewBag.FormatType = formatType;
        _formatType = formatType;
        return View("Index");
    }

    public IActionResult DownloadDocument()
    {
        // Retrieve the JSON data from the ViewBag
        var jsonData = _formattedData;
        var contentType = "application/json";
        var fileName = "data.json";

        if (_formatType == "XMLFormatter")
        {
            contentType = "text/xml";
            fileName = "data.xml";
        }

        Response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");
        Response.ContentType = contentType;
        
        // Write the JSON data to the response body
        return Content(jsonData, contentType);
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