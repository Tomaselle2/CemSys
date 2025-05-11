using CemSys.Interface;
using CemSys.Interface.Panteones;
using CemSys.Models;

namespace CemSys.Business
{
    public class PanteonesBusiness : IPanteonesBusiness
    {
        private readonly IRepositoryDB<SeccionesPanteone> _seccionesPanteonesrepositoryDB;
        private readonly IRepositoryDB<Panteone> _panteonesrepositoryDB;


        public PanteonesBusiness(IRepositoryDB<SeccionesPanteone> repositoryDBPanteon, IRepositoryDB<Panteone> panteonesrepositoryDB)
        {
            _seccionesPanteonesrepositoryDB = repositoryDBPanteon;
            _panteonesrepositoryDB = panteonesrepositoryDB;
        }

        public async Task<SeccionesPanteone> ConsultarSeccion(int id)
        {
            try
            {
                return await _seccionesPanteonesrepositoryDB.Consultar(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task CrearPanteones(SeccionesPanteone modelo)
        {
            for (int i = 1; i <= modelo.Panteones; i++)
            {
                Panteone panteon = new Panteone();
                panteon.NroLote = i;
                panteon.IdSeccionPanteon = modelo.IdSeccionPanteones;
                panteon.Visibilidad = true;
                panteon.Difuntos = 0;

                try
                {
                    await _panteonesrepositoryDB.Registrar(panteon);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<List<Panteone>> ListaPanteones(int id)
        {
            try
            {
                List<Panteone> listaCompleta = await _panteonesrepositoryDB.EmitirListado();
                return listaCompleta.Where(opc => opc.IdSeccionPanteon == id).ToList();

            }
            catch
            {
                throw;
            }
        }
    }
}
