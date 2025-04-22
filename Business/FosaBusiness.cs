using CemSys.Data;
using CemSys.Interface;
using CemSys.Interface.Fosas;
using CemSys.Models;

namespace CemSys.Business
{
    public class FosaBusiness : IFosasBusiness
    {
        private readonly IRepositoryDB<Fosa> _repositoryDB;
        private readonly IRepositoryDB<SeccionesFosa> _repositorySeccionFosaDB;
        private readonly FosaBD _fosaBD;

        public FosaBusiness(IRepositoryDB<Fosa> repositoryDB, IRepositoryDB<SeccionesFosa> repositorySeccionFosaDB)
        {
            _repositoryDB = repositoryDB;
            _repositorySeccionFosaDB = repositorySeccionFosaDB;
        }

        public async Task<SeccionesFosa> ConsultarSeccionFosa(int id)
        {
            return await _repositorySeccionFosaDB.Consultar(id);
        }

        public async Task CrearFosas(SeccionesFosa modelo)
        {
            for(int i=1; i <= modelo.Fosas; i++)
            {
                Fosa fosa = new Fosa();
                fosa.NroFosa = i;
                fosa.Seccion = modelo.IdSeccionFosa;
                fosa.Visibilidad = true;
                fosa.Difuntos = 0;

                try
                {
                    await _repositoryDB.Registrar(fosa);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<List<Fosa>> ListaFosas()
        {
            return await _repositoryDB.EmitirListado();
        }

        public async Task<SeccionesFosa> ObtenerSeccionFosaPorNombre(string nombre)
        {
            return await _fosaBD.ObtenerSeccionFosaPorNombre(nombre);
        }

    }
}
