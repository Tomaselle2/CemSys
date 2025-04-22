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

            viewModel.seccion = await _fosasBusiness.ConsultarSeccionFosa(id);
            viewModel.ABMRepositoryVM.Lista = await _fosasBusiness.ListaFosas();
            return View(viewModel);
        }

        public async Task<IActionResult> Registrar(string nombre)
        {
            try
            {
                SeccionesFosa modelo = await _fosasBusiness.ObtenerSeccionFosaPorNombre(nombre);
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
