using CemSys.Interface;
using CemSys.Models;
using CemSys.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CemSys.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IRepositoryBusiness<Usuario> _usuarioRepositoryBusiness;
        private readonly IRepositoryBusiness<TipoUsuario> _tipoUsuarioRepositoryBusiness;
        public UsuarioController(IRepositoryBusiness<Usuario> usuarioRepositoryBusiness, IRepositoryBusiness<TipoUsuario> tipoUsuarioRepositoryBusiness)
        {
            _usuarioRepositoryBusiness = usuarioRepositoryBusiness;
            _tipoUsuarioRepositoryBusiness = tipoUsuarioRepositoryBusiness;
        }

        VMUsuarios viewModel = new VMUsuarios();

        public async Task<IActionResult> Index( VMUsuarios viewModel)
        {
            if (viewModel.ABMRepositoryVM.Lista.Count == 0)
            {
                viewModel.ABMRepositoryVM.Lista = await _usuarioRepositoryBusiness.EmitirListado();
                viewModel.ListaTipoUsuario = await _tipoUsuarioRepositoryBusiness.EmitirListado();
                viewModel.ABMRepositoryVM.EsModificacion = false;
            }
            
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(string nombre, string correo, string clave, int idTipoUsuario)
        {
            int resultado = 0;

            Usuario modelo = new Usuario();
            modelo.Nombre = nombre;
            modelo.Correo = correo;
            modelo.Clave = clave;
            modelo.TipoUsuario = idTipoUsuario;

            try
            {
                resultado = await _usuarioRepositoryBusiness.Registrar(modelo);
                ViewData["MensajeRegistrar"] = "Exito al registrar";
            }
            catch (Exception ex)
            {
                ViewData["MensajeRegistrar"] = ex.Message;
                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Modificar(int id)
        {
            Usuario modelo = await _usuarioRepositoryBusiness.Consultar(id);
            viewModel.ABMRepositoryVM.Modelo = modelo;
            viewModel.ABMRepositoryVM.EsModificacion = true;
            viewModel.ABMRepositoryVM.Lista = await _usuarioRepositoryBusiness.EmitirListado();
            viewModel.ListaTipoUsuario = await _tipoUsuarioRepositoryBusiness.EmitirListado();


            return View("Index", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Modificar(string nombre, string correo, string clave, int idTipoUsuario, int id)
        {
            int resultado = 0;
            Usuario modelo = await _usuarioRepositoryBusiness.Consultar(id);
            modelo.Nombre = nombre;
            modelo.Correo = correo;
            modelo.Clave = clave;
            modelo.TipoUsuario = idTipoUsuario;
            try
            {
                resultado = await _usuarioRepositoryBusiness.Modificar(modelo);
                ViewData["MensajeModificar"] = "Modificado con éxito";

            }
            catch (Exception ex)
            {
                ViewData["MensajeModificar"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _usuarioRepositoryBusiness.Eliminar(id);
                ViewData["MensajeEliminar"] = "Exito al eliminar";

            }
            catch (Exception ex)
            {
                ViewData["MensajeEliminar"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

    }
}
