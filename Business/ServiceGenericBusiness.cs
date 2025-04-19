using CemSys.Data;
using CemSys.Interface;
using CemSys.Models;

namespace CemSys.Business
{
    public class ServiceGenericBusiness<T> : IRepositoryBusiness<T> where T : class
    {
        private readonly IRepositoryDB<T> _contextDB;
        public ServiceGenericBusiness(IRepositoryDB<T> context)
        {
            _contextDB = context;
        }

        public async Task<T> Consultar(int id)
        {
            try
            {
                return await _contextDB.Consultar(id);
            }
            catch (Exception) { throw; }
        }

        public async Task Eliminar(int id)
        {
            try
            {
                await _contextDB.Eliminar(id);
            }
            catch (Exception) { throw; }
        }

        public async Task<List<T>> EmitirListado()
        {
            try
            {
                return await _contextDB.EmitirListado();
            }
            catch (Exception) { throw; }
        }

        public async Task<int> Modificar(T modelo)
        {
            try
            {
                return await _contextDB.Modificar(modelo);
            }
            catch (Exception) { throw; }
        }

        public async Task<int> Registrar(T modelo)
        {
            try
            {
                return await _contextDB.Registrar(modelo);
            }
            catch (Exception) { throw; }
        }
    }
}
