﻿using Common.Shared;
using Microsoft.AspNetCore.Mvc;
using SearchWebApp.Models;
using System.Diagnostics;

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
    public ActionResult Search(string input)
    {
        var searchWord = _apiClient.GetSearchData(input);
        ViewBag.prevInput = input;
        return View("Index", searchWord);
    }
    [HttpGet]
    public ActionResult FormatData(string formatType)
    {

        var formatResult = _apiClient.GetFormattedData(formatType);
        ViewBag.prevInput = formatType;
        return View("Index", formatResult);
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