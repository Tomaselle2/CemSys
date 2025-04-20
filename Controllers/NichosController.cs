using CemSys.Interface;
using CemSys.Interface.Nichos;
using CemSys.Models;
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

        public async Task<IActionResult> Index(int id)
        {
            SeccionesNicho seccion = await _nichosBusiness.ObtenerSeccionNicho(id);
            return View(seccion);
        }

        public async Task<IActionResult> Registrar(string nombre) {
            try
            {
                SeccionesNicho modelo = await _nichosBusiness.ObtenerSeccionNichoPorNombre(nombre);
                if(modelo.TipoNumeracion == 5)
                {
                    await _nichosBusiness.CrearNichosNumeracionNueva(modelo);
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
