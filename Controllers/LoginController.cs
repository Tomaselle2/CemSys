using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IniciarSesion(string correo, string clave)
        {
            string contra = "user";
            string correoelectronico = "user@gmail.com";

            if(correo == correoelectronico && contra == clave)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Login"] = "Correo o contraseña incorrecta";
                return View("Index");
            }


           
        }
    }
}
