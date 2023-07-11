using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Utilities;

namespace WebMVC.Controllers;

public class HomeController : WebMVCBaseController<HomeController>
{

    public HomeController(ILogger<HomeController> logger) : base(logger) { }

    public async Task<IActionResult> Index()
    {
        var response = await GetFromJsonAsync<TodoViewModel>("/todos/1");
        return View(response);
    }

}



