using Microsoft.AspNetCore.Mvc;
using CemSys.Models.ViewModel;
using CemSys.Interface.Contratos;

namespace CemSys.Controllers
{
    public class ContratosController : Controller
    {
        private readonly IContratosBusiness _contratosBusiness;

        public ContratosController(IContratosBusiness contratosBusiness)
        {
            _contratosBusiness = contratosBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Registrar()
        {
            var nombre = HttpContext.Session.GetString("usuario");
            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["UsuarioLogueado"] = nombre;

            // Llamada asincrónica con await
            var contrato = await _contratosBusiness.ConsultarContratoPorCuenta("12345"); // ejemplo

            return View(); // si necesitas pasar datos, hazlo aquí
        }

    }
}

