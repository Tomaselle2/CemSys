using CemSys.Interface;
using CemSys.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CemSys.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRepositoryBusiness<Usuario> _usuarioRepositoryBusiness;
        public LoginController(IRepositoryBusiness<Usuario> usuarioRepositoryBusiness)
        {
            _usuarioRepositoryBusiness = usuarioRepositoryBusiness;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("idUsuario") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            List<Usuario> usuarios = await _usuarioRepositoryBusiness.EmitirListado();

            foreach (var usuario in usuarios)
            {
                if (correo == usuario.Correo && clave == usuario.Clave)
                {
                    HttpContext.Session.SetInt32("idUsuario", usuario.IdUsuario);
                    HttpContext.Session.SetString("nombreUsuario", usuario.Nombre);
                    HttpContext.Session.SetInt32("tipoUsuario", usuario.TipoUsuario);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Login"] = "Correo o contraseña incorrecta";
                    return View("Index");
                }
            }

            return View("Index");
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
