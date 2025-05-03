using CemSys.Interface.Difuntos;
using CemSys.Models;
using CemSys.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CemSys.Controllers
{
    public class DifuntosController : Controller
    {
        VMDifuntos viewModel = new VMDifuntos();
        private readonly IDifuntosBusiness _difuntosBusiness;

        public DifuntosController(IDifuntosBusiness difuntosBusiness)
        {
            _difuntosBusiness = difuntosBusiness;
        }

        public IActionResult Index()
        {
            //login
            var nombre = HttpContext.Session.GetString("nombreUsuario");
            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombre;

           

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(
            string dni,
            string nombre,
            string apellido,
            DateOnly defuncionFecha,
            DateOnly ingresoFecha,
            DateOnly nacimientoFecha,
            int estadoId,
            int acta,
            int tomo,
            int folio,
            string serie,
            int age,
            string tipoParcela,
            int seccionElegida,
            int parcelaElegida
            ) {
            //login
            var nombreLog = HttpContext.Session.GetString("nombreUsuario");
            if (nombreLog == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombreLog;
            //fin login

            //registra el acta de defuncion
            ActaDefuncion modeloActa = new ActaDefuncion();
            modeloActa.NroActa = acta;
            modeloActa.Tomo = tomo;
            modeloActa.Folio = folio;
            modeloActa.Serie = serie;
            modeloActa.Age = age;
            int idActaDefuncionGenerado = 0;
            try
            {
                idActaDefuncionGenerado = await _difuntosBusiness.RegistrarActaDefuncion(modeloActa);
            }catch(Exception ex)
            {
                ViewData["RegistrarActa"] = ex.Message;
            }

            //registra el difunto
            Difunto modelo = new Difunto();
            modelo.Nombre = nombre;
            modelo.Apellido = apellido;
            modelo.Dni = dni;
            modelo.FechaNacimiento = nacimientoFecha;
            modelo.FechaDefuncion = defuncionFecha;
            modelo.FechaIngreso = ingresoFecha;
            modelo.Estado = estadoId;
            modelo.ActaDefuncion = idActaDefuncionGenerado;
            modelo.Visibilidad = true;

            int idDifuntoGenerado = 0;
            
            try
            {
                idDifuntoGenerado = await _difuntosBusiness.RegistrarDifunto(modelo); //registra al difunto
            }
            catch(Exception ex)
            {
                ViewData["RegistrarMensaje"] = ex.Message;
            }

            //registra en tabla intermedia nichoDifunto
            
            switch (tipoParcela)
            {
                case "nicho":

                    NichosDifunto nichoDifuntoModelo = new NichosDifunto();
                    nichoDifuntoModelo.NichoId = parcelaElegida;
                    nichoDifuntoModelo.DifuntoId = idDifuntoGenerado;
                    int idNichoDifuntoGenerado = 0;
                    try
                    {
                        idDifuntoGenerado = await _difuntosBusiness.RegistrarEnNicho(nichoDifuntoModelo);
                        Nicho nichoseleccionado = await _difuntosBusiness.ConsultarNicho(parcelaElegida);
                        nichoseleccionado.Difuntos = nichoseleccionado.Difuntos + 1;
                        int resultadoAgregarNicho = 0;
                        resultadoAgregarNicho = await _difuntosBusiness.IncrementarDifuntoEnNicho(nichoseleccionado);
                    }
                    catch(Exception ex)
                    {
                        ViewData["RegistrarMensaje"] = ex.Message;
                    }



                    break;
                case "fosa":
                    break;
                case "panteon":
                    break;
            }

            return RedirectToAction("Registrar");
        }

        [HttpGet]
        public async Task<IActionResult> Registrar(VMDifuntos viewModel)
        {
            //login
            var nombre = HttpContext.Session.GetString("nombreUsuario");
            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombre;
            //fin login
            DateOnly year = DateOnly.FromDateTime(DateTime.Now);


            if (viewModel != null)
            {
                try
                {
                    viewModel.ListaEstadoDifunto = await _difuntosBusiness.EmitirListadoEstadoDifunto();//lista todos los difuntos en la vista
                    viewModel.ListaSeccionesNicho = await _difuntosBusiness.EmitirListadoSeccionesNicho();//lista todas las secc de nichos en la vista
                    viewModel.ListaSeccionesFosa = await _difuntosBusiness.EmitirListadoSeccionesFosa();//lista todas las secc de fosas en la vista
                    viewModel.ListaNichos = await _difuntosBusiness.EmitirListadoNichos();//lista todos los nichos en la vista
                    viewModel.ListaFosas = await _difuntosBusiness.EmitirListadoFosas();//lista todos las fosas en la vista
                    viewModel.fechaActual = year;
                    viewModel.ListaDifuntos = await _difuntosBusiness.EmitirListadoDifuntos();
                }
                catch(Exception ex)
                {
                    ViewData["MensajeError"] = ex.Message;
                }
               
            }

            return View(viewModel);
        }
    }
    
}
