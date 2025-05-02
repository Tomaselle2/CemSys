using CemSys.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class DifuntosController : Controller
    {
        VMDifuntos viewModel = new VMDifuntos();
        public IActionResult Index()
        {
            //login
            var nombre = HttpContext.Session.GetString("nombreUsuario");
            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombre;


            return View();
        }

        [HttpPost]
        public IActionResult Registrar(
            string dni,
            string nombre,
            string apellido,
            DateOnly defuncionFecha,
            DateOnly ingresoFecha,
            DateOnly nacimientoFecha,
            int estadoId,
            int acta,
            int tomo,
            int folio,
            string serie,
            int age,
            string tipoParcela,
            int seccionElegida,
            int nichoElegido
            ) {
            //login
            var nombreLog = HttpContext.Session.GetString("nombreUsuario");
            if (nombreLog == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombreLog;
            //fin login
            return View();
        }
    }
}
