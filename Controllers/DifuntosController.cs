using CemSys.Interface.Difuntos;
using CemSys.Models;
using CemSys.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<IActionResult> Index( VM_Introduccion_Listado viewModelListadoFiltrado)
        {
            //login
            var nombre = HttpContext.Session.GetString("nombreUsuario");
            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombre;


            viewModelListadoFiltrado.ListaSeccionesNichos = await _difuntosBusiness.EmitirListadoSeccionesNicho();
            viewModelListadoFiltrado.ListaSeccionesFosas = await _difuntosBusiness.EmitirListadoSeccionesFosa();
            viewModelListadoFiltrado.ListaSeccionesPanteones = await _difuntosBusiness.EmitirListadoSeccionesPanteon();
            



            return View(viewModelListadoFiltrado);
        }

        [HttpGet]
        public async Task<IActionResult> Filtrar(string? nombreDifunto, string? apellidoDifunto, string? tipoParcela, int? seccionElegida, string? dniDifunto)
        {
            VM_Introduccion_Listado viewModelListadoFiltrado = await Filtrador(nombreDifunto, apellidoDifunto, tipoParcela, seccionElegida, dniDifunto);
            viewModelListadoFiltrado.ListaSeccionesNichos = await _difuntosBusiness.EmitirListadoSeccionesNicho();
            viewModelListadoFiltrado.ListaSeccionesFosas = await _difuntosBusiness.EmitirListadoSeccionesFosa();
            viewModelListadoFiltrado.ListaSeccionesPanteones = await _difuntosBusiness.EmitirListadoSeccionesPanteon();
            return View("Index",viewModelListadoFiltrado);
        }


        private async Task<VM_Introduccion_Listado> Filtrador(string? nombreDifunto, string? apellidoDifunto, string? tipoParcela, int? seccionElegida, string? dniDifunto)
        {
            var viewModelListado = new VM_Introduccion_Listado();

            try
            {
                var listaNichos = await _difuntosBusiness.EmitirListadoNichosDifuntos();
                var listaFosas = await _difuntosBusiness.EmitirListadoFosasDifuntos();
                var listaPanteones = await _difuntosBusiness.EmitirListadoPanteonDifuntos();

                //filtra por dni
                if (!string.IsNullOrEmpty(dniDifunto))
                {
                    listaNichos = listaNichos
                        .Where(x => (x.Difunto.Dni).ToLower().Contains(dniDifunto.ToLower()))
                        .ToList();

                    listaFosas = listaFosas
                        .Where(x => (x.Difunto.Dni).ToLower().Contains(dniDifunto.ToLower()))
                        .ToList();

                    listaPanteones = listaPanteones
                        .Where(x => (x.Difunto.Dni).ToLower().Contains(dniDifunto.ToLower()))
                        .ToList();
                }

                //filtra por nombre
                if (!string.IsNullOrEmpty(nombreDifunto))
                {
                    listaNichos = listaNichos
                        .Where(x => (x.Difunto.Nombre).ToLower().Contains(nombreDifunto.ToLower()))
                        .ToList();

                    listaFosas = listaFosas
                        .Where(x => (x.Difunto.Nombre).ToLower().Contains(nombreDifunto.ToLower()))
                        .ToList();

                    listaPanteones = listaPanteones
                        .Where(x => (x.Difunto.Nombre).ToLower().Contains(nombreDifunto.ToLower()))
                        .ToList();
                }


                //filtra por apellido
                if (!string.IsNullOrEmpty(apellidoDifunto))
                {
                    listaNichos = listaNichos
                        .Where(x => (x.Difunto.Apellido).ToLower().Contains(apellidoDifunto.ToLower()))
                        .ToList();

                    listaFosas = listaFosas
                        .Where(x => (x.Difunto.Apellido).ToLower().Contains(apellidoDifunto.ToLower()))
                        .ToList();

                    listaPanteones = listaPanteones
                        .Where(x => (x.Difunto.Apellido).ToLower().Contains(apellidoDifunto.ToLower()))
                        .ToList();
                }

                // FILTRO POR TIPO
                if (!string.IsNullOrEmpty(tipoParcela))
                {
                    if (tipoParcela == "nicho")
                    {
                        listaFosas = new List<FosasDifunto>();
                        listaPanteones = new List<PanteonDifunto>();
                    }
                    else if (tipoParcela == "fosa")
                    {
                        listaNichos = new List<NichosDifunto>();
                        listaPanteones = new List<PanteonDifunto>();
                    }
                    else if (tipoParcela == "panteon")
                    {
                        listaNichos = new List<NichosDifunto>();
                        listaFosas = new List<FosasDifunto>();
                    }
                    else
                    {
                        //SELECCIONO OPCION TODAS
                    }
                }

                // FILTRO POR SECCIÓN
                if (seccionElegida != null)
                {
                    listaNichos = listaNichos
                        .Where(x => x.Nicho.Seccion == seccionElegida)
                        .ToList();

                    listaFosas = listaFosas
                        .Where(x => x.Fosa.Seccion == seccionElegida)
                        .ToList();

                    listaPanteones = listaPanteones
                        .Where(x => x.Panteon.IdSeccionPanteon == seccionElegida)
                        .ToList();
                }

                viewModelListado.ListaNichosDifuntos = listaNichos;
                viewModelListado.ListaFosasDifuntos = listaFosas;
                viewModelListado.ListaPanteonDifuntos = listaPanteones;

            }
            catch (Exception e)
            {
                ViewData["MensajeError"] = e.Message;
            }

            return viewModelListado;

        }

        [HttpGet]
        public async Task<IActionResult> Modificar(int idDifunto)
        {
            try
            {
                viewModel.ABMRepositoryVM.Modelo = await _difuntosBusiness.ConsultarDifunto(idDifunto);
                viewModel.ListaDifuntos = await _difuntosBusiness.EmitirListadoDifuntos();
                viewModel.ABMRepositoryVM.EsModificacion = true;
                DateOnly year = DateOnly.FromDateTime(DateTime.Now);
                viewModel.fechaActual = year;

            }
            catch(Exception ex)
            {
                ViewData["MensajeError"] = ex.Message;
            }
            return View("Registrar", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Modificar(int idDifunto, string dni,
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
            string datosAdicionales)
        {

            try
            {

                //modificar
                Difunto modelo = await _difuntosBusiness.ConsultarDifunto(idDifunto);
                if (nombre != null)
                {
                    modelo.Nombre = nombre.ToLower();
                }
                else
                {
                    modelo.Nombre = "NN";
                }
                modelo.Apellido = apellido.ToLower();
                if (dni != null)
                {
                    modelo.Dni = dni;
                }
                else { modelo.Dni = "nn"; }
                modelo.FechaNacimiento = nacimientoFecha;
                modelo.FechaDefuncion = defuncionFecha;
                modelo.FechaIngreso = ingresoFecha;
                modelo.Estado = estadoId;
                modelo.ActaDefuncionNavigation.NroActa = acta;
                modelo.ActaDefuncionNavigation.Tomo = tomo;
                modelo.ActaDefuncionNavigation.Folio = folio;
                
                if (serie != null)
                {
                    modelo.ActaDefuncionNavigation.Serie = serie.ToLower();
                }
                else
                {
                    modelo.ActaDefuncionNavigation.Serie = serie;
                }
                modelo.ActaDefuncionNavigation.Age = age;

                modelo.Visibilidad = true;
                modelo.Descripcion = datosAdicionales;

                int idmodificado = await _difuntosBusiness.ModificarDifunto(modelo);

                //llena los datos
                viewModel.ABMRepositoryVM.Modelo = await _difuntosBusiness.ConsultarDifunto(idDifunto);
                viewModel.ListaDifuntos = await _difuntosBusiness.EmitirListadoDifuntos();
                viewModel.ABMRepositoryVM.EsModificacion = true;
                DateOnly year = DateOnly.FromDateTime(DateTime.Now);
                viewModel.fechaActual = year;
                TempData["ModificarMensaje"] = "Modificación exitosa";

            }
            catch (Exception ex)
            {
                TempData["ModificarMensaje"] = ex.Message;
            }
            return View("Registrar", viewModel);
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
            int parcelaElegida,
            string datosAdicionales
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
            if (serie != null)
            {
                modeloActa.Serie = serie.ToLower();
            }
            else
            {
                modeloActa.Serie = serie;
            }
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
            if(nombre != null)
            {
                modelo.Nombre = nombre.ToLower();
            }
            else
            {
                modelo.Nombre = "NN";
            }
            modelo.Apellido = apellido.ToLower();
            if(dni != null)
            {
                modelo.Dni = dni;
            }
            else { modelo.Dni = "nn"; }
            modelo.FechaNacimiento = nacimientoFecha;
            modelo.FechaDefuncion = defuncionFecha;
            modelo.FechaIngreso = ingresoFecha;
            modelo.Estado = estadoId;
            modelo.ActaDefuncion = idActaDefuncionGenerado;
            modelo.Visibilidad = true;
            modelo.Descripcion = datosAdicionales;

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
                        //registra en la tabla intermedia
                        idNichoDifuntoGenerado = await _difuntosBusiness.RegistrarEnNicho(nichoDifuntoModelo);

                        //busco el nicho para ingrementar el nro de difuntos
                        Nicho nichoseleccionado = await _difuntosBusiness.ConsultarNicho(parcelaElegida);
                        nichoseleccionado.Difuntos = nichoseleccionado.Difuntos + 1;

                        //registro el incremento
                        int resultadoAgregarNicho = 0;
                        resultadoAgregarNicho = await _difuntosBusiness.IncrementarDifuntoEnNicho(nichoseleccionado);
                        TempData["RegistrarMensaje"] = "Registro exitoso";
                    }
                    catch(Exception ex)
                    {
                        TempData["RegistrarMensaje"] = ex.Message;
                    }



                    break;
                case "fosa":
                    FosasDifunto fosasDifunto = new FosasDifunto();
                    fosasDifunto.DifuntoId = idDifuntoGenerado;
                    fosasDifunto.FosaId = parcelaElegida;
                    int fosaDifuntoGenerado = 0;

                    try
                    {
                        //registra en la tabla intermedia
                        fosaDifuntoGenerado = await _difuntosBusiness.RegistrarEnFosa(fosasDifunto);

                        //busco la fosa para ingrementar el nro de difuntos
                        Fosa fosaseleccionada = await _difuntosBusiness.ConsultarFosa(parcelaElegida);
                        fosaseleccionada.Difuntos = fosaseleccionada.Difuntos + 1;

                        //registro el incremento
                        int resultadoAgregarFosa = 0;
                        resultadoAgregarFosa = await _difuntosBusiness.IncrementarDifuntoEnFosa(fosaseleccionada);
                        TempData["RegistrarMensaje"] = "Registro exitoso";

                    }
                    catch (Exception ex)
                    {
                        TempData["RegistrarMensaje"] = ex.Message;
                    }
                    break;
                case "panteon":
                    PanteonDifunto panteonDifunto = new PanteonDifunto();
                    panteonDifunto.DifuntoId = idDifuntoGenerado;
                    panteonDifunto.PanteonId = parcelaElegida;
                    int panteonDifuntoGenerado = 0;

                    try
                    {
                        //registra en la tabla intermedia
                        panteonDifuntoGenerado = await _difuntosBusiness.RegistrarEnPanteon(panteonDifunto);

                        //busco el panteon para ingrementar el nro de difuntos
                        Panteone panteonSeleccionado = await _difuntosBusiness.ConsultarPanteon(parcelaElegida);
                        panteonSeleccionado.Difuntos = panteonSeleccionado.Difuntos + 1;

                        //registro el incremento
                        int resultadoAgregarpanteon = 0;
                        resultadoAgregarpanteon = await _difuntosBusiness.IncrementarDifuntoEnPanteon(panteonSeleccionado);
                        TempData["RegistrarMensaje"] = "Registro exitoso";
                    }
                    catch (Exception ex)
                    {
                        TempData["RegistrarMensaje"] = ex.Message;
                    }
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
           


            if (viewModel != null)
            {
                try
                {
                   await CargarContenidoInicialenPantallaRegistrar(viewModel);
                }
                catch (Exception ex)
                {
                    ViewData["MensajeError"] = ex.Message;
                }
               
            }

            return View(viewModel);
        }

        private async Task CargarContenidoInicialenPantallaRegistrar(VMDifuntos viewModel)
        {
            DateOnly year = DateOnly.FromDateTime(DateTime.Now);

            viewModel.ListaEstadoDifunto = await _difuntosBusiness.EmitirListadoEstadoDifunto();//lista todos los difuntos en la vista
            viewModel.ListaSeccionesNicho = await _difuntosBusiness.EmitirListadoSeccionesNicho();//lista todas las secc de nichos en la vista
            viewModel.ListaSeccionesFosa = await _difuntosBusiness.EmitirListadoSeccionesFosa();//lista todas las secc de fosas en la vista
            viewModel.ListaSeccionesPanteon = await _difuntosBusiness.EmitirListadoSeccionesPanteon();//lista todas las secc de panteones en la vista

            viewModel.ListaNichos = await _difuntosBusiness.EmitirListadoNichos();//lista todos los nichos en la vista
            viewModel.ListaFosas = await _difuntosBusiness.EmitirListadoFosas();//lista todos las fosas en la vista
            viewModel.ListaPanteones = await _difuntosBusiness.EmitirListadoPanteones();//lista todos los panteones en la vista

            viewModel.fechaActual = year;
            viewModel.ListaDifuntos = await _difuntosBusiness.EmitirListadoDifuntos();
            viewModel.ABMRepositoryVM.EsModificacion = false;
        }

    
    }
    
}
