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
        private readonly IRepositoryDB<Nicho> _nichoRepository;
        private readonly IRepositoryDB<Fosa> _fosaRepository;
        private readonly IRepositoryDB<ActaDefuncion> _actaRepository;
        private readonly IRepositoryDB<NichosDifunto> _nichoDifuntoRepository;


        public DifuntosBusiness(IRepositoryDB<Difunto> difuntosbd, IRepositoryDB<EstadoDifunto> estadodifuntoRepository, IRepositoryDB<SeccionesNicho> seccionesNichosRepository, 
            IRepositoryDB<SeccionesFosa> seccionesFosasRepository, IRepositoryDB<Nicho> nichoRepository, IRepositoryDB<Fosa> fosaRepository, IRepositoryDB<ActaDefuncion> actaRepository,
            IRepositoryDB<NichosDifunto> nichoDifuntoRepository)
        {
            _difuntosRepository = difuntosbd;
            _estadodifuntoRepository = estadodifuntoRepository;
            _seccionesNichosRepository = seccionesNichosRepository;
            _seccionesFosasRepository = seccionesFosasRepository;
            _nichoRepository = nichoRepository;
            _fosaRepository = fosaRepository;
            _actaRepository = actaRepository;
            _nichoDifuntoRepository = nichoDifuntoRepository;
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

        public async Task<List<DTO_seccionesFosa>> EmitirListadoSeccionesFosa()
        {
            try
            {
                return (await _seccionesFosasRepository.EmitirListado()).Where(s => s.Visibilidad == true).Select(s => new DTO_seccionesFosa
                {
                    Id = s.IdSeccionFosa,
                    Nombre = s.Nombre,
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
                    Nombre = s.Nombre,
                    Visibilidad = s.Visibilidad
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> IncrementarDifuntoEnFosa(Nicho modelo)
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

        public Task<int> IncrementarDifuntoEnPanteon(Nicho modelo)
        {
            throw new NotImplementedException();
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

        public Task<int> RegistrarEnFosa(FosasDifunto modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<int> RegistrarEnNicho(NichosDifunto modelo)
        {
            try
            {
                return await _nichoDifuntoRepository.Registrar(modelo);
            }
            catch (Exception) { throw; }
        }

        public Task<int> RegistrarEnPanteon(PanteonDifunto modelo)
        {
            throw new NotImplementedException();
        }

        //registrar difunto
        //listado estado difunto
        //listado secciones nichos
        //listado secciones fosas
        //listado nichos
        //listado fosas
        //registrar acta

    }
}
