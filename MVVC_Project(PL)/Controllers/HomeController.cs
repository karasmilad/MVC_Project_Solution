using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVVC_Project_PL_.Models;
using System.Diagnostics;

namespace MVVC_Project_PL_.Controllers
{
    [Authorize] //Anyony is Authenticated Can Access this Controller
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
