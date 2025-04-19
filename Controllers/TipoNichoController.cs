using CemSys.Interface;
using CemSys.Models;
using CemSys.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class TipoNichoController : Controller
    {
        private readonly IRepositoryBusiness<TipoNicho> _serviceBusiness;
        public TipoNichoController(IRepositoryBusiness<TipoNicho> tipoNichoBusiness)
        {
            _serviceBusiness = tipoNichoBusiness;
        }

        ABMRepositoryVM<TipoNicho> viewModel = new ABMRepositoryVM<TipoNicho> ();

        public async Task<IActionResult> Index(ABMRepositoryVM<TipoNicho> viewModel)
        {
            if(viewModel.Lista.Count == 0)
            {
                viewModel.Lista = await EmitirListado();
                viewModel.EsModificacion = false;
                
            }
            return View(viewModel);
        }

        private async Task<List<TipoNicho>> EmitirListado()
        {
            List<TipoNicho> lista = new List<TipoNicho>();
            try
            {
                lista = await _serviceBusiness.EmitirListado();
                ViewData["MensajeEmitir"] = "Exito al emitir listado";
            }
            catch (Exception ex)
            {
                ViewData["MensajeEmitir"] = ex.Message;
            }

            return lista;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(string tipo) {
            int resultado = 0;

            TipoNicho modelo = new TipoNicho();
            modelo.TipoNicho1 = tipo;

            try
            {
                resultado = await _serviceBusiness.Registrar(modelo);
                ViewData["MensajeRegistrar"] = "Exito al registrar";
            }
            catch (Exception ex)
            {
                ViewData["MensajeRegistrar"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
               await _serviceBusiness.Eliminar(id);
                ViewData["MensajeEliminar"] = "Exito al eliminar";

            }
            catch (Exception ex)
            {
                ViewData["MensajeEliminar"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> Modificar(string tipo, int id)
        {
            int resultado = 0;
            TipoNicho modelo = await _serviceBusiness.Consultar(id);
            modelo.TipoNicho1 = tipo;
            

            try
            {
               resultado = await _serviceBusiness.Modificar(modelo);
               ViewData["MensajeModificar"] = "Modificado con éxito";

            }
            catch (Exception ex)
            {
                ViewData["MensajeModificar"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Modificar(int id)
        {
            TipoNicho modelo = await _serviceBusiness.Consultar(id);
            viewModel.Modelo = modelo;
            viewModel.EsModificacion = true;
            viewModel.Lista = await EmitirListado();

            return View("Index", viewModel);
        }
        
    }
}
