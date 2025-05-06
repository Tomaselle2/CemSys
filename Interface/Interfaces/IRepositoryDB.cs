using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CemSys.Interface
{
    public interface IRepositoryDB<T> where T : class
    {
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllAsync();
        Task<T> Registrar(T entity);
        Task<T> Modificar(T entity);
        Task Eliminar(int id);
        Task<T?> Consultar(int id);
    }
}

