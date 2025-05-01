using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class PanteonesController : Controller
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
    }
}
