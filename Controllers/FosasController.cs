using CemSys.Interface.Fosas;
using CemSys.Models;
using CemSys.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CemSys.Controllers
{
    public class FosasController : Controller
    {
        private readonly IFosasBusiness _fosasBusiness;
        public FosasController(IFosasBusiness fosasBusiness)
        {
            _fosasBusiness = fosasBusiness;
        }

        VMFosas viewModel = new VMFosas();

        public async Task<IActionResult> Index(int id)
        {
            var nombre = HttpContext.Session.GetString("nombreUsuario");

            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombre;

            viewModel.seccion = await _fosasBusiness.ConsultarSeccionFosa(id);
            viewModel.ABMRepositoryVM.Lista = await _fosasBusiness.ListaFosas(id);
            return View(viewModel);
        }

        public async Task<IActionResult> Registrar(int idFosaSeccion)
        {
            try
            {
                SeccionesFosa modelo = await _fosasBusiness.ConsultarSeccionFosa(idFosaSeccion);
                await _fosasBusiness.CrearFosas(modelo);
            }
            catch (Exception ex)
            {
                ViewData["MessageError"] = ex.Message;
            }



            return RedirectToAction("Index", "SeccionesFosas");
        }
    }
}
