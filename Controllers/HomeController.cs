using CemSys.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CemSys.Controllers
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
            var nombre = HttpContext.Session.GetString("nombreUsuario");

            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }
                

            ViewData["UsuarioLogueado"] = nombre;
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
