using Microsoft.AspNetCore.Mvc;
using SearchWebApp.Models;
using System.Diagnostics;

namespace SearchWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public ActionResult Search(string input)
    {
        var searchWord = ApiClient.GetSearchData(input);
        ViewBag.prevInput = input;
        return View("Index", searchWord);
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