using CemSys.Interface.Panteones;
using CemSys.Models;
using CemSys.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class PanteonesController : Controller
    {
        private readonly IPanteonesBusiness _panteonesBusiness;
        public PanteonesController(IPanteonesBusiness panteonesBusiness)
        {
            _panteonesBusiness = panteonesBusiness;
        }
        VMPanteones viewModel = new VMPanteones();

        public async Task<IActionResult> Index(int id)
        {
            var nombre = HttpContext.Session.GetString("nombreUsuario");
            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombre;

            viewModel.seccion = await _panteonesBusiness.ConsultarSeccion(id);
            viewModel.ABMRepositoryVM.Lista = await _panteonesBusiness.ListaPanteones(id);
            return View(viewModel);

        }

        public async Task<IActionResult> Registrar(int idseccionPanteon)
        {
            try
            {
                SeccionesPanteone secc = await _panteonesBusiness.ConsultarSeccion(idseccionPanteon);
                await _panteonesBusiness.CrearPanteones(secc);
            }
            catch (Exception ex)
            {
                ViewData["MessageError"] = ex.Message;
            }

            return RedirectToAction("Index", "SeccionesPanteones");
        }
    }
}
