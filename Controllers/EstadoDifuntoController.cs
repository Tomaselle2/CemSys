using CemSys.Interface;
using CemSys.Models;
using CemSys.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class EstadoDifuntoController : Controller
    {
        private readonly IRepositoryBusiness<EstadoDifunto> _serviceBusiness;
        public EstadoDifuntoController(IRepositoryBusiness<EstadoDifunto> estadoNichoBusiness)
        {
            _serviceBusiness = estadoNichoBusiness;
        }

        ABMRepositoryVM<EstadoDifunto> viewModel = new ABMRepositoryVM<EstadoDifunto>();

        public async Task<IActionResult> Index(ABMRepositoryVM<EstadoDifunto> viewModel)
        {
            var nombre = HttpContext.Session.GetString("nombreUsuario");
            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombre;

            if (viewModel.Lista.Count == 0)
            {
                viewModel.Lista = await EmitirListado();
                viewModel.EsModificacion = false;

            }
            return View(viewModel);
        }

        private async Task<List<EstadoDifunto>> EmitirListado()
        {
            List<EstadoDifunto> lista = new List<EstadoDifunto>();
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
        public async Task<IActionResult> Registrar(string tipo)
        {
            int resultado = 0;

            EstadoDifunto modelo = new EstadoDifunto();
            modelo.Estado = tipo;

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
            EstadoDifunto modelo = await _serviceBusiness.Consultar(id);
            modelo.Estado = tipo;


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
            EstadoDifunto modelo = await _serviceBusiness.Consultar(id);
            viewModel.Modelo = modelo;
            viewModel.EsModificacion = true;
            viewModel.Lista = await EmitirListado();

            return View("Index", viewModel);
        }

    }
}
