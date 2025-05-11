using CemSys.Interface;
using CemSys.Models;
using CemSys.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CemSys.Controllers
{
    public class SeccionesPanteonesController : Controller
    {
        private readonly IRepositoryBusiness<SeccionesPanteone> _serviceBusiness;

        public SeccionesPanteonesController(IRepositoryBusiness<SeccionesPanteone> repositoryBusiness)
        {
            _serviceBusiness = repositoryBusiness;
        }

        ABMRepositoryVM<SeccionesPanteone> viewModel = new ABMRepositoryVM<SeccionesPanteone>();

        public async Task<IActionResult> Index(ABMRepositoryVM<SeccionesPanteone> viewModel)
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

        private async Task<List<SeccionesPanteone>> EmitirListado()
        {
            List<SeccionesPanteone> lista = new List<SeccionesPanteone>();
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
        public async Task<IActionResult> Registrar(string nombre, int nroLotes)
        {
            int idSeccionePanteonGenerada = 0;

            SeccionesPanteone modelo = new SeccionesPanteone();
            modelo.Nombre = nombre.ToLower();
            modelo.Panteones = nroLotes;
            modelo.Visibilidad = true;

            try
            {
                idSeccionePanteonGenerada = await _serviceBusiness.Registrar(modelo);

                ViewData["MensajeRegistrar"] = "Exito al registrar";
                //pasa el id de la seccion generada
                return PasarDatosCrearPanteones(idSeccionePanteonGenerada);

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
                SeccionesPanteone secc = await _serviceBusiness.Consultar(id);
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

        private IActionResult PasarDatosCrearPanteones(int idseccionPanteon)
        {
            return RedirectToAction("Registrar", "Panteones", new { idseccionPanteon = idseccionPanteon });
        }

        [HttpGet]
        public IActionResult Administrar(int id)
        {
            return RedirectToAction("Index", "Panteones", new { id = id });
        }
    }
}
