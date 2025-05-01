using CemSys.Interface.SeccionesNichos;
using CemSys.Interface;
using CemSys.Models.ViewModel;
using CemSys.Models;
using Microsoft.AspNetCore.Mvc;
using CemSys.Interface.Fosas;

namespace CemSys.Controllers
{
    public class SeccionesFosasController : Controller
    {
        private readonly IRepositoryBusiness<SeccionesFosa> _serviceBusiness;
        private readonly IFosasBusiness _fosasBusiness;

        public SeccionesFosasController(IRepositoryBusiness<SeccionesFosa> seccionesFosaBusiness, IFosasBusiness fosasBusiness)
        {
            _serviceBusiness = seccionesFosaBusiness;
            _fosasBusiness = fosasBusiness;
        }

        ABMRepositoryVM<SeccionesFosa> viewModel = new ABMRepositoryVM<SeccionesFosa>();

        public async Task<IActionResult> Index(ABMRepositoryVM<SeccionesFosa> viewModel)
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

        private async Task<List<SeccionesFosa>> EmitirListado()
        {
            List<SeccionesFosa> lista = new List<SeccionesFosa>();
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
        public async Task<IActionResult> Registrar(string nombre, int nroFosas)
        {
            int resultado = 0;

            SeccionesFosa modelo = new SeccionesFosa();
            modelo.Nombre = nombre;
            modelo.Fosas = nroFosas;
            modelo.Visibilidad = true;

            try
            {
                resultado = await _serviceBusiness.Registrar(modelo);

                ViewData["MensajeRegistrar"] = "Exito al registrar";
                //pasa el nombre de la seccion generada
                return PasarDatosCrearFosas(nombre);

            }
            catch (Exception ex)
            {
                ViewData["MensajeRegistrar"] = ex.Message;
                return RedirectToAction("Index");

            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                SeccionesFosa secc = await _serviceBusiness.Consultar(id);
                secc.Visibilidad = false;

                await _serviceBusiness.Modificar(secc);
                ViewData["MensajeEliminar"] = "Exito al eliminar";

            }
            catch (Exception ex)
            {
                ViewData["MensajeEliminar"] = ex.Message;
            }

            return RedirectToAction("Index");
        }


        private IActionResult PasarDatosCrearFosas(string nombre)
        {
            return RedirectToAction("Registrar", "Fosas", new { nombre = nombre });
        }

        [HttpGet]
        public IActionResult Administrar(int id)
        {
            return RedirectToAction("Index", "Fosas", new { id = id });
        }
    }
}
