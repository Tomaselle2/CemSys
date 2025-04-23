using CemSys.Interface;
using CemSys.Interface.Nichos;
using CemSys.Models;

namespace CemSys.Business
{
    public class NichosBusiness : INichosBusiness
    {
        private readonly IRepositoryDB<SeccionesNicho> _seccionesNichoRepository;
        private readonly IRepositoryDB<Nicho> _nichoRepository;
        private readonly IRepositoryDB<TipoNicho> _tipoNichoRepository;

        private readonly INichosDB _nichosDB;
        public NichosBusiness(IRepositoryDB<SeccionesNicho> seccionesNichoRepository, INichosDB nichosDB, IRepositoryDB<Nicho> repositoryDB, IRepositoryDB<TipoNicho> tipoNichoRepository)
        {
            _seccionesNichoRepository = seccionesNichoRepository;
            _nichosDB = nichosDB;
            _nichoRepository = repositoryDB;
            _tipoNichoRepository = tipoNichoRepository;
        }
        public async Task CrearNichosNumeracionAntigua(SeccionesNicho modelo)
        {
            int filas = modelo.Filas;
            int columnas = modelo.Nichos / modelo.Filas;
            int nroNichoContador = 1;
 
                for (int i = 1; i <= filas; i++)
                {
                    for (int j = 1; j <= columnas; j++)
                    {
                        Nicho nicho = new Nicho();
                        nicho.NroFila = i;
                        nicho.NroNicho = nroNichoContador;
                        nicho.Visibilidad = true;
                        nicho.Difuntos = 0;
                        nicho.TipoNicho = 1;
                        nicho.Seccion = modelo.IdSeccionNicho;

                        try
                        {
                            await _nichoRepository.Registrar(nicho);
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        nroNichoContador++; // Aumenta el contador después de cada nicho
                    }
                }
            
            
        }

        public async Task CrearNichosNumeracionNueva(SeccionesNicho modelo)
        {
            int columnas = modelo.Nichos / modelo.Filas;
            for (int i = 1; i <= modelo.Filas; i++)
            {
                for(int j = 1; j <= columnas; j++)
                {
                    Nicho nicho = new Nicho();
                    nicho.NroFila = i;
                    nicho.NroNicho = j;
                    nicho.Visibilidad = true;
                    nicho.Difuntos = 0;
                    nicho.TipoNicho = 2;
                    nicho.Seccion = modelo.IdSeccionNicho;

                    try
                    {
                        await _nichoRepository.Registrar(nicho);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            
        }

        public async Task<List<Nicho>> ListaDeNichos(int id)
        {
            List<Nicho> listaCompleta = await _nichoRepository.EmitirListado();
            return listaCompleta.Where(opc => opc.Seccion == id).ToList();
        }

        public async Task<List<TipoNicho>> ListaTipoNicho()
        {
            return await _tipoNichoRepository.EmitirListado();
        }

        public async Task<SeccionesNicho> ObtenerSeccionNicho(int id)
        {
            SeccionesNicho seccion = await _seccionesNichoRepository.Consultar(id);
            return seccion;
        }

        public async Task<SeccionesNicho> ObtenerSeccionNichoPorNombre(string nombre)
        {
            return await _nichosDB.ObtenerSeccionNichoPorNombre(nombre);
        }
    }
}
