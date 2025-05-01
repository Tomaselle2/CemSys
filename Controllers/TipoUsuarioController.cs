using CemSys.Interface;
using CemSys.Models.ViewModel;
using CemSys.Models;
using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class TipoUsuarioController : Controller
    {
        private readonly IRepositoryBusiness<TipoUsuario> _serviceBusiness;
        public TipoUsuarioController(IRepositoryBusiness<TipoUsuario> tipoUsuarioBusiness)
        {
            _serviceBusiness = tipoUsuarioBusiness;
        }

        ABMRepositoryVM<TipoUsuario> viewModel = new ABMRepositoryVM<TipoUsuario>();
        public async Task<IActionResult> Index(ABMRepositoryVM<TipoUsuario> viewModel)
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

        private async Task<List<TipoUsuario>> EmitirListado()
        {
            List<TipoUsuario> lista = new List<TipoUsuario>();
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

            TipoUsuario modelo = new TipoUsuario();
            modelo.TipoUsuario1 = tipo;

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
            TipoUsuario modelo = await _serviceBusiness.Consultar(id);
            modelo.TipoUsuario1 = tipo;


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
            TipoUsuario modelo = await _serviceBusiness.Consultar(id);
            viewModel.Modelo = modelo;
            viewModel.EsModificacion = true;
            viewModel.Lista = await EmitirListado();

            return View("Index", viewModel);
        }
    }
}
