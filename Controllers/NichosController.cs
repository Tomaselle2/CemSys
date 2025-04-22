using CemSys.Interface;
using CemSys.Interface.Nichos;
using CemSys.Models;
using CemSys.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class NichosController : Controller
    {
        private readonly IRepositoryBusiness<Nicho> _serviceBusiness;
        private readonly INichosBusiness _nichosBusiness;
        public NichosController(IRepositoryBusiness<Nicho> serviceBusiness, INichosBusiness nichosBusiness)
        {
            _serviceBusiness = serviceBusiness;
            _nichosBusiness = nichosBusiness; 
        }
        VMNichos viewModel = new VMNichos();

        public async Task<IActionResult> Index(int id)
        {
            viewModel.seccion = await _nichosBusiness.ObtenerSeccionNicho(id);
            viewModel.ABMRepositoryVM.Lista = await _nichosBusiness.ListaDeNichos(id);
            viewModel.ListaTipoNicho = await _nichosBusiness.ListaTipoNicho();
            return View(viewModel);
        }

        public async Task<IActionResult> Registrar(string nombre) {
            try
            {
                SeccionesNicho modelo = await _nichosBusiness.ObtenerSeccionNichoPorNombre(nombre);
                if(modelo.TipoNumeracion == 1)
                {
                    await _nichosBusiness.CrearNichosNumeracionNueva(modelo);
                }

                if (modelo.TipoNumeracion == 2)
                {
                    await _nichosBusiness.CrearNichosNumeracionAntigua(modelo);
                }

            }
            catch (Exception ex)
            {
                ViewData["MessageError"] = ex.Message;   
            }

 

            return RedirectToAction("Index", "SeccionesNichos");
        }
    }
}
