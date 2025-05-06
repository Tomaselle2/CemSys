using CemSys.Interface;
using CemSys.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CemSys.Data.Repositories
{
    public class RepositoryDB<T> : IRepositoryDB<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryDB(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public interface IRepositoryDB<T> where T : class
        {
            Task<T> Registrar(T entity);
            // ... resto de métodos
        }

        public async Task<T> Modificar(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Eliminar(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T?> Consultar(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
