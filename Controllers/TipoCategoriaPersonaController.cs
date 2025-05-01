using CemSys.Interface;
using CemSys.Models.ViewModel;
using CemSys.Models;
using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class TipoCategoriaPersonaController : Controller
    {
        private readonly IRepositoryBusiness<TipoCategoriaPersona> _serviceBusiness;
        public TipoCategoriaPersonaController(IRepositoryBusiness<TipoCategoriaPersona> tipoCategoriaPersonaNichoBusiness)
        {
            _serviceBusiness = tipoCategoriaPersonaNichoBusiness;
        }

        ABMRepositoryVM<TipoCategoriaPersona> viewModel = new ABMRepositoryVM<TipoCategoriaPersona>();
        public async Task<IActionResult> Index(ABMRepositoryVM<TipoCategoriaPersona> viewModel)
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

        private async Task<List<TipoCategoriaPersona>> EmitirListado()
        {
            List<TipoCategoriaPersona> lista = new List<TipoCategoriaPersona>();
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

            TipoCategoriaPersona modelo = new TipoCategoriaPersona();
            modelo.Tipo = tipo;

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
            TipoCategoriaPersona modelo = await _serviceBusiness.Consultar(id);
            modelo.Tipo = tipo;


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
            TipoCategoriaPersona modelo = await _serviceBusiness.Consultar(id);
            viewModel.Modelo = modelo;
            viewModel.EsModificacion = true;
            viewModel.Lista = await EmitirListado();

            return View("Index", viewModel);
        }
    }
}
