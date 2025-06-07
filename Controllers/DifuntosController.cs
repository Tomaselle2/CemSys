using CemSys.Interface.Difuntos;
using CemSys.Models;
using CemSys.Models.ViewModel;
using HeyRed.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Rotativa.AspNetCore;
using System.Threading.Tasks;

namespace CemSys.Controllers
{
    public class DifuntosController : Controller
    {
        VMDifuntos viewModel = new VMDifuntos();
        private readonly IDifuntosBusiness _difuntosBusiness;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DifuntosController(IDifuntosBusiness difuntosBusiness, IWebHostEnvironment webHostEnvironment)
        {
            _difuntosBusiness = difuntosBusiness;
            _webHostEnvironment = webHostEnvironment;
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

                int contador = 0;
                foreach (var opc in listaNichos)
                {
                    contador++;
                }
                foreach (var opc in listaFosas)
                {
                    contador++;
                }
                foreach (var opc in listaPanteones)
                {
                    contador++;
                }

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
                viewModelListado.CantidadIntroducciones = contador;

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
            string datosAdicionales,
            string fechaIngreso,
            int empleadoResponsable,
            string empresaCargo
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
            bool parseExitoso;
            DateTime fechaIngresoDateTime;
            parseExitoso = DateTime.TryParse(fechaIngreso, out fechaIngresoDateTime);
            int idTramite = 0;
            string tipoParcelaResumen = tipoParcela;

            switch (tipoParcela)
            {
                case "nicho":
                    NichosDifunto nichoDifuntoModelo = new NichosDifunto();
                    nichoDifuntoModelo.NichoId = parcelaElegida;
                    nichoDifuntoModelo.DifuntoId = idDifuntoGenerado;
                    
                    if (parseExitoso)
                    {
                        nichoDifuntoModelo.FechaIngreso = fechaIngresoDateTime;
                    }
                    nichoDifuntoModelo.Visibilidad = true; //por defecto es visible
                    nichoDifuntoModelo.Empresa = empresaCargo;
                    nichoDifuntoModelo.Usuario = empleadoResponsable;

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

                        //registra el tramite
                        Tramite modeloTramite = new Tramite();
                        modeloTramite.IdNichosDifuntosFk = idNichoDifuntoGenerado;
                        idTramite = await _difuntosBusiness.RegistrarTramite(modeloTramite);
                        TempData["RegistrarMensaje"] = "Introducción realizada con éxito";
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

                    if (parseExitoso)
                    {
                        fosasDifunto.FechaIngreso = fechaIngresoDateTime;
                    }
                    fosasDifunto.Visibilidad = true; //por defecto es visible
                    fosasDifunto.Empresa = empresaCargo;
                    fosasDifunto.Usuario = empleadoResponsable;
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

                        //registra el tramite
                        Tramite modeloTramite = new Tramite();
                        modeloTramite.IdFosasDifuntosFk = fosaDifuntoGenerado;
                        idTramite = await _difuntosBusiness.RegistrarTramite(modeloTramite);
                        TempData["RegistrarMensaje"] = "Introducción realizada con éxito";

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
                    if (parseExitoso)
                    {
                        panteonDifunto.FechaIngreso = fechaIngresoDateTime;
                    }
                    panteonDifunto.Visibilidad = true; //por defecto es visible
                    panteonDifunto.Empresa = empresaCargo;
                    panteonDifunto.Usuario = empleadoResponsable;
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

                        //registra el tramite
                        Tramite modeloTramite = new Tramite();
                        modeloTramite.IdPanteonesDifuntos = panteonDifuntoGenerado;
                        idTramite = await _difuntosBusiness.RegistrarTramite(modeloTramite);
                        TempData["RegistrarMensaje"] = "Introducción realizada con éxito";
                    }
                    catch (Exception ex)
                    {
                        TempData["RegistrarMensaje"] = ex.Message;
                    }
                    break;
            }

            return RedirectToAction("ResumenIntroduccion", new { idtramite = idTramite, tipoParcelaResumen = tipoParcela });
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
            viewModel.ListaUsuarios = await _difuntosBusiness.ListaUsuarios();//lista todos los usuarios en la vista

            viewModel.fechaActual = year;
            viewModel.ListaDifuntos = await _difuntosBusiness.EmitirListadoDifuntos();
            viewModel.ABMRepositoryVM.EsModificacion = false;
            viewModel.ABMRepositoryVM.Modelo = new Difunto();
            viewModel.ABMRepositoryVM.Modelo.Nombre = "";
            viewModel.ABMRepositoryVM.Modelo.Apellido = "";
            viewModel.ABMRepositoryVM.Modelo.Dni = "";
            viewModel.ABMRepositoryVM.Modelo.FechaDefuncion = null;
            viewModel.ABMRepositoryVM.Modelo.FechaNacimiento = null;
            viewModel.ABMRepositoryVM.Modelo.ActaDefuncionNavigation = new ActaDefuncion();
            viewModel.ABMRepositoryVM.Modelo.ActaDefuncionNavigation.NroActa = 0;
            viewModel.ABMRepositoryVM.Modelo.ActaDefuncionNavigation.Tomo = 0;
            viewModel.ABMRepositoryVM.Modelo.ActaDefuncionNavigation.Folio = 0;
            viewModel.ABMRepositoryVM.Modelo.ActaDefuncionNavigation.Serie = "";
            viewModel.ABMRepositoryVM.Modelo.ActaDefuncionNavigation.Age = 0;
            viewModel.ABMRepositoryVM.Modelo.Estado = 0;
            viewModel.ABMRepositoryVM.Modelo.Descripcion = "";
            viewModel.ABMRepositoryVM.Modelo.Visibilidad = true;

        }

         private async Task<VMResumenIntroduccion> TraerDatosDetallaIntroduccion(int idtramite)
         {
             VMResumenIntroduccion viewModelResumen = new VMResumenIntroduccion();
             Tramite tramite = new Tramite();
             TramiteViewModel modelo = new TramiteViewModel();

            try
            {
                tramite = await _difuntosBusiness.ConsultarTramite(idtramite);

                modelo = new TramiteViewModel
                {
                    IdTramite = tramite.IdTramite,

                    idNichoDifuntoFK = tramite.IdNichosDifuntosFk,
                    idFosaDifuntoFK = tramite.IdFosasDifuntosFk,
                    idPanteonDifuntoFK = tramite.IdPanteonesDifuntos,

                    nichosDifuntos = tramite.IdNichosDifuntosFkNavigation,
                    fosasDifuntos = tramite.IdFosasDifuntosFkNavigation,
                    panteonesDifuntos = tramite.IdPanteonesDifuntosNavigation
                };
                viewModelResumen.ListaTramites.Add(modelo);
            }
            catch (Exception ex)
            {
                 ViewData["MensajeError"] = ex.Message;
            }

             return viewModelResumen;
         } 

        [HttpGet]
        public async Task<IActionResult> ResumenIntroduccion(int idtramite)
        {
            //login
            var nombreLog = HttpContext.Session.GetString("nombreUsuario");
            if (nombreLog == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombreLog;
            //fin login

            VMResumenIntroduccion viewModelResumen = await TraerDatosDetallaIntroduccion(idtramite);
            return View(viewModelResumen);
        }

        [HttpGet]
        public async Task<IActionResult> ResumenIntroduccionEnPDF(int idtramite)
        {
            //login
            var nombreLog = HttpContext.Session.GetString("nombreUsuario");
            if (nombreLog == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombreLog;
            //fin login

            Dictionary<string, string> fotos = new();
            string rutaImagenes = Path.Combine(_webHostEnvironment.WebRootPath, "fotos");
            string[] arhivos = Directory.GetFiles(rutaImagenes);
            string nombre, mimeType;
            byte[] data;

            foreach (string item in arhivos)
            {
                nombre = Path.GetFileNameWithoutExtension(item);
                mimeType = MimeTypesMap.GetMimeType(item);
                data = System.IO.File.ReadAllBytes(item);
                fotos.Add(nombre, string.Concat("data:", mimeType, ";base64,", Convert.ToBase64String(data)));
            }
            var viewData = ViewData;
            viewData["fotos"] = fotos;
            VMResumenIntroduccion viewModelResumen = await TraerDatosDetallaIntroduccion(idtramite);
            return new ViewAsPdf(viewModelResumen)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins(10, 5, 5, 10),
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                FileName = $"Tramite {viewModelResumen.ListaTramites[0].IdTramite}.pdf",
                ViewData = viewData
            };
        }

        [HttpGet]
        public async Task<IActionResult> IndexIntroduccion()
        {             
            //login
            var nombre = HttpContext.Session.GetString("nombreUsuario");
            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombre;
            //fin login
            VMResumenIntroduccion viewModel = new VMResumenIntroduccion();
            try
            {

                viewModel.ListaTramites = (await _difuntosBusiness.EmitirListadoTramites())
                    .Select(t => new TramiteViewModel
                    {
                        IdTramite = t.IdTramite,
                        idNichoDifuntoFK = t.IdNichosDifuntosFk,
                        idFosaDifuntoFK = t.IdFosasDifuntosFk,
                        idPanteonDifuntoFK = t.IdPanteonesDifuntos,

                        nichosDifuntos = t.IdNichosDifuntosFkNavigation,
                        fosasDifuntos = t.IdFosasDifuntosFkNavigation,
                        panteonesDifuntos = t.IdPanteonesDifuntosNavigation
                    })
                    .ToList();



            }
            catch (Exception ex)
            {
                ViewData["MensajeError"] = ex.Message;
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> IndexIntroduccion(string tipoFiltro, DateOnly? desde, DateOnly? hasta)
        {
            //login
            var nombre = HttpContext.Session.GetString("nombreUsuario");
            if (nombre == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["UsuarioLogueado"] = nombre;
            //fin login
            VMResumenIntroduccion viewModelResumen = new VMResumenIntroduccion();


            try
            {
  
                viewModelResumen.ListaTramites = (await _difuntosBusiness.EmitirListadoTramites())
                    .Select(t => new TramiteViewModel
                    {
                        IdTramite = t.IdTramite,
                        idNichoDifuntoFK = t.IdNichosDifuntosFk,
                        idFosaDifuntoFK = t.IdFosasDifuntosFk,
                        idPanteonDifuntoFK = t.IdPanteonesDifuntos,

                        nichosDifuntos = t.IdNichosDifuntosFkNavigation,
                        fosasDifuntos = t.IdFosasDifuntosFkNavigation,
                        panteonesDifuntos = t.IdPanteonesDifuntosNavigation
                    })
                    .ToList();

                

            }catch (Exception ex)
            {
                ViewData["MensajeError"] = ex.Message;
            }
            
            return View(viewModelResumen);
        }


    }
    
}
