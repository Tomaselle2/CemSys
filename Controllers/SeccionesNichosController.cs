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

        VMSeccionesNichos viewModel = new VMSeccionesNichos();

        public async Task<IActionResult> Index(VMSeccionesNichos viewModel)
        {
            var nombre = HttpContext.Session.GetString("nombreUsuario");
            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombre;

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
            modelo.Nombre = nombre.ToLower();
            modelo.Nichos = nroNichos;
            modelo.Filas = nroFilas;
            modelo.TipoNumeracion = idTipoNumeracionParela;
            modelo.Visibilidad = true;

            try
            {
                resultado = await _serviceBusiness.Registrar(modelo);
             
                ViewData["MensajeRegistrar"] = "Exito al registrar";
                   //pasa el nombre de la seccion generada
                return PasarDatosCrearNichos(resultado);
             
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
                SeccionesNicho secc = await _serviceBusiness.Consultar(id);
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



        private async Task<List<TipoNumeracionParcela>> ListaTipoNumeracionParcela()
        {
            return await _seccionesNichosBusiness.ListaTipoNumeracionParcela();
        }

        private IActionResult PasarDatosCrearNichos(int idSeccionNicho)
        {
            return RedirectToAction("Registrar", "Nichos", new { idSeccionNicho = idSeccionNicho });
        }

        [HttpGet]
        public IActionResult Administrar(int id)
        {
            return RedirectToAction("Index", "Nichos", new {id = id});
        }
    }
}
