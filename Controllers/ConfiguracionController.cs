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
            var nombre = HttpContext.Session.GetString("nombreUsuario");

            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }


            ViewData["UsuarioLogueado"] = nombre;
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

        public IActionResult SeccionesPanteones()
        {
            return RedirectToAction("Index", "SeccionesPanteones");
        }

        public IActionResult Usuario()
        {
            return RedirectToAction("Index", "Usuario");
        }
        public IActionResult EstadoDifunto()
        {
            return RedirectToAction("Index", "EstadoDifunto");
        }


    }
}
