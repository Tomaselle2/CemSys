using CemSys.Interface;
using CemSys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CemSys.Controllers
{
    public class ConfiguracionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TipoNicho()
        {
            return RedirectToAction("Index", "TipoNicho");
        }

        public IActionResult TipoNumeracionParcela()
        {
            return RedirectToAction("Index", "TipoNumeracionParcela");
        }

        public IActionResult SeccionesNichos()
        {
            return RedirectToAction("Index", "SeccionesNichos");
        }

        public IActionResult SeccionesFosas()
        {
            return RedirectToAction("Index", "SeccionesFosas");
        }

        public IActionResult TipoCategoriaPersona()
        {
            return RedirectToAction("Index", "TipoCategoriaPersona");
        }

        public IActionResult TipoUsuario()
        {
            return RedirectToAction("Index", "TipoUsuario");
        }

        public IActionResult Panteones()
        {
            return RedirectToAction("Index", "Panteones");
        }


    }
}
