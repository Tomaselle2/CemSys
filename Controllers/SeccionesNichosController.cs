using CemSys.Interface;
using CemSys.Interface.SeccionesNichos;
using CemSys.Models;
using CemSys.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class SeccionesNichosController : Controller
    {
        private readonly IRepositoryBusiness<SeccionesNicho> _serviceBusiness;
        private readonly ISeccionesNichosBusiness _seccionesNichosBusiness;
        public SeccionesNichosController(IRepositoryBusiness<SeccionesNicho> seccionesNichoBusiness, ISeccionesNichosBusiness seccinesNichosBusiness)
        {
            _serviceBusiness = seccionesNichoBusiness;
            _seccionesNichosBusiness = seccinesNichosBusiness;
        }

        //ABMRepositoryVM<SeccionesNicho> viewModel = new ABMRepositoryVM<SeccionesNicho>();
        VMSeccionesNichos viewModel = new VMSeccionesNichos();

        public async Task<IActionResult> Index(VMSeccionesNichos viewModel)
        {
            if (viewModel.ABMRepositoryVM.Lista.Count == 0)
            {
                viewModel.ABMRepositoryVM.Lista = await EmitirListado();
                viewModel.ABMRepositoryVM.EsModificacion = false;
                viewModel.ListaNumeracionParcelas = await ListaTipoNumeracionParcela();

            }
            return View(viewModel);
        }

        private async Task<List<SeccionesNicho>> EmitirListado()
        {
            List<SeccionesNicho> lista = new List<SeccionesNicho>();
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
        public async Task<IActionResult> Registrar(string nombre, int nroNichos, int nroFilas, int idTipoNumeracionParela)
        {
            int resultado = 0;

            SeccionesNicho modelo = new SeccionesNicho();
            modelo.Nombre = nombre;
            modelo.Nichos = nroNichos;
            modelo.Filas = nroFilas;
            modelo.TipoNumeracion = idTipoNumeracionParela;
            modelo.Visibilidad = true;

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
            SeccionesNicho modelo = await _serviceBusiness.Consultar(id);
            modelo.Nombre = tipo;


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
            SeccionesNicho modelo = await _serviceBusiness.Consultar(id);
            viewModel.ABMRepositoryVM.Modelo = modelo;
            viewModel.ABMRepositoryVM.EsModificacion = true;
            viewModel.ABMRepositoryVM.Lista = await EmitirListado();

            return View("Index", viewModel);
        }

        private async Task<List<TipoNumeracionParcela>> ListaTipoNumeracionParcela()
        {
            return await _seccionesNichosBusiness.ListaTipoNumeracionParcela();
        }
    }
}
