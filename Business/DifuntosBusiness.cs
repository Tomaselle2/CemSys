using CemSys.Interface;
using CemSys.Interface.Difuntos;
using CemSys.Models;
using CemSys.Models.DTO;

namespace CemSys.Business
{
    public class DifuntosBusiness : IDifuntosBusiness
    {
        private readonly IRepositoryDB<Difunto> _difuntosRepository;
        private readonly IRepositoryDB<EstadoDifunto> _estadodifuntoRepository;
        private readonly IRepositoryDB<SeccionesNicho> _seccionesNichosRepository;
        private readonly IRepositoryDB<SeccionesFosa> _seccionesFosasRepository;
        private readonly IRepositoryDB<SeccionesPanteone> _seccionesPanteonesRepository;

        private readonly IRepositoryDB<Nicho> _nichoRepository;
        private readonly IRepositoryDB<Fosa> _fosaRepository;
        private readonly IRepositoryDB<Panteone> _panteonRepository;
        private readonly IRepositoryDB<ActaDefuncion> _actaRepository;

        private readonly IRepositoryDB<NichosDifunto> _nichoDifuntoRepository;
        private readonly IRepositoryDB<FosasDifunto> _fosaDifuntoRepository;
        private readonly IRepositoryDB<PanteonDifunto> _panteonDifuntoRepository;

        private readonly IIntroducion_datos _introduccionDatosParcelaDifunto;




        public DifuntosBusiness(IRepositoryDB<Difunto> difuntosbd, IRepositoryDB<EstadoDifunto> estadodifuntoRepository, IRepositoryDB<SeccionesNicho> seccionesNichosRepository, 
            IRepositoryDB<SeccionesFosa> seccionesFosasRepository, IRepositoryDB<Nicho> nichoRepository, IRepositoryDB<Fosa> fosaRepository, IRepositoryDB<ActaDefuncion> actaRepository,
            IRepositoryDB<NichosDifunto> nichoDifuntoRepository, IRepositoryDB<FosasDifunto> fosaDifuntoRepository, IRepositoryDB<Panteone> panteonRepository,
            IRepositoryDB<SeccionesPanteone> seccionesPanteonesRepository, IRepositoryDB<PanteonDifunto> panteonDifuntoRepository, IIntroducion_datos introduccionDatosParcelaDifunto)
        {
            _difuntosRepository = difuntosbd;
            _estadodifuntoRepository = estadodifuntoRepository;
            _seccionesNichosRepository = seccionesNichosRepository;
            _seccionesFosasRepository = seccionesFosasRepository;
            _nichoRepository = nichoRepository;
            _fosaRepository = fosaRepository;
            _actaRepository = actaRepository;
            _nichoDifuntoRepository = nichoDifuntoRepository;
            _fosaDifuntoRepository = fosaDifuntoRepository;
            _panteonRepository = panteonRepository;
            _seccionesPanteonesRepository = seccionesPanteonesRepository;
            _panteonDifuntoRepository = panteonDifuntoRepository;
            _introduccionDatosParcelaDifunto = introduccionDatosParcelaDifunto;
        }

        public async Task<Fosa> ConsultarFosa(int id)
        {
            try
            {
                return await _fosaRepository.Consultar(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Nicho> ConsultarNicho(int id)
        {
            try
            {
                return await _nichoRepository.Consultar(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Panteone> ConsultarPanteon(int id)
        {
            try
            {
                return await _panteonRepository.Consultar(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DTO_difunto>> EmitirListadoDifuntos()
        {
            try
            {
                return (await _difuntosRepository.EmitirListado()).Where(s => s.Visibilidad == true && s.Dni != null).Select(s => new DTO_difunto
                {
                    Visibilidad = s.Visibilidad,
                    DNI = s.Dni
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<EstadoDifunto>> EmitirListadoEstadoDifunto()
        {
            try
            {
                return await _estadodifuntoRepository.EmitirListado();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DTO_fosas>> EmitirListadoFosas()
        {
            try
            {
                return (await _fosaRepository.EmitirListado()).Where(s => s.Visibilidad == true).Select(s => new DTO_fosas
                {
                    Id = s.IdFosa,
                    Visibilidad = s.Visibilidad,
                    Difuntos = s.Difuntos,
                    Seccion = s.Seccion,
                    Ubicacion = $"Fosa {s.NroFosa}"
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FosasDifunto>> EmitirListadoFosasDifuntos()
        {
            try
            {
                return await _introduccionDatosParcelaDifunto.EmitirListadoFosasDifuntos();
            }
            catch (Exception) { throw; }
        }

        public async Task<List<DTO_nichos>> EmitirListadoNichos()
        {
            try
            {
                return (await _nichoRepository.EmitirListado()).Where(s => s.Visibilidad == true).Select(s => new DTO_nichos
                {
                    Id = s.IdNicho,
                    Visibilidad = s.Visibilidad,
                    Difuntos = s.Difuntos,
                    Seccion = s.Seccion,
                    TipoNicho = s.TipoNicho,
                    Ubicacion = $"Nicho {s.NroNicho} Fila {s.NroFila}"
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NichosDifunto>> EmitirListadoNichosDifuntos()
        {       
            try
            {

                return await _introduccionDatosParcelaDifunto.EmitirListadoNichosDifuntos();
            }
            catch (Exception) { throw; }
        }

        public async Task<List<PanteonDifunto>> EmitirListadoPanteonDifuntos()
        {
            try
            {
                return await _introduccionDatosParcelaDifunto.EmitirListadoPanteonDifuntos();
            }
            catch (Exception) { throw; }
        }

        public async Task<List<DTO_panteones>> EmitirListadoPanteones()
        {
            try
            {
                return (await _panteonRepository.EmitirListado()).Where(s => s.Visibilidad == true).Select(s => new DTO_panteones
                {
                    Id = s.IdPanteon,
                    Visibilidad = s.Visibilidad,
                    Difuntos = s.Difuntos,
                    Seccion = s.IdSeccionPanteon,
                    Ubicacion = $"Lote {s.NroLote}"
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DTO_seccionesFosa>> EmitirListadoSeccionesFosa()
        {
            try
            {
                return (await _seccionesFosasRepository.EmitirListado()).Where(s => s.Visibilidad == true).Select(s => new DTO_seccionesFosa
                {
                    Id = s.IdSeccionFosa,
                    Nombre = s.Nombre.ToUpper(),
                    Visibilidad = s.Visibilidad
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DTO_seccionesNicho>> EmitirListadoSeccionesNicho()
        {
            try
            {
                return (await _seccionesNichosRepository.EmitirListado()).Where(s => s.Visibilidad == true).Select(s => new DTO_seccionesNicho
                {
                    Id = s.IdSeccionNicho,
                    Nombre = s.Nombre.ToUpper(),
                    Visibilidad = s.Visibilidad
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DTO_seccionesPanteon>> EmitirListadoSeccionesPanteon()
        {
            try
            {
                return (await _seccionesPanteonesRepository.EmitirListado()).Where(s => s.Visibilidad == true).Select(s => new DTO_seccionesPanteon
                {
                    Id = s.IdSeccionPanteones,
                    Nombre = s.Nombre.ToUpper(),
                    Visibilidad = s.Visibilidad
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> IncrementarDifuntoEnFosa(Fosa modelo)
        {
            try
            {
                return await _fosaRepository.Modificar(modelo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> IncrementarDifuntoEnNicho(Nicho modelo)
        {
            try
            {
                return await _nichoRepository.Modificar(modelo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> IncrementarDifuntoEnPanteon(Panteone modelo)
        {
            try
            {
                return await _panteonRepository.Modificar(modelo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> RegistrarActaDefuncion(ActaDefuncion modeloacta)
        {
            try
            {
                return await _actaRepository.Registrar(modeloacta);
            }
            catch (Exception) { throw; }
        }

        public async Task<int> RegistrarDifunto(Difunto modelo)
        {
            try
            {
                return await _difuntosRepository.Registrar(modelo);
            }
            catch (Exception) { throw; }
        }

        public async Task<int> RegistrarEnFosa(FosasDifunto modelo)
        {
            try
            {
                return await _fosaDifuntoRepository.Registrar(modelo);
            }
            catch (Exception) { throw; }
        }

        public async Task<int> RegistrarEnNicho(NichosDifunto modelo)
        {
            try
            {
                return await _nichoDifuntoRepository.Registrar(modelo);
            }
            catch (Exception) { throw; }
        }

        public async Task<int> RegistrarEnPanteon(PanteonDifunto modelo)
        {
            try
            {
                return await _panteonDifuntoRepository.Registrar(modelo);
            }
            catch (Exception) { throw; }
        }


    }
}
