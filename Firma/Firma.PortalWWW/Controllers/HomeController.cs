using System.Diagnostics;
using Firma.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;

namespace Firma.PortalWWW.Controllers
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
        public IActionResult Przeglad() {
            return View();
        }
        public IActionResult Szczegoly()
        {
            return View();
        }
        public IActionResult Kontakt()
        {
            return View();
        }
        public IActionResult Regulamin()
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
