using IStaTP_Lab1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IStaTP_Lab1.Controllers
{
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Categories()
        {
            return RedirectToAction("Index", "Categories");
        }

        public IActionResult Authors()
        {
            return RedirectToAction("Index", "Authors");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Readers()
        {
            return RedirectToAction("Index", "Readers");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}