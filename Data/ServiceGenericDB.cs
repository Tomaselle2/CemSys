using System.Linq.Expressions;
using CemSys.Interface;
using CemSys.Models;
using Microsoft.EntityFrameworkCore;

namespace CemSys.Data
{
    public class ServiceGenericDB<T> : IRepositoryDB<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public ServiceGenericDB(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> Consultar(int id)

        {
            try
            {
                var modelo = await _dbSet.FindAsync(id);
                return modelo!;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Eliminar(int id)
        {
            try
            {
                var modelo = await Consultar(id);
                if (modelo != null)
                {
                    _dbSet.Remove(modelo);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<List<T>> EmitirListado()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Modificar(T modelo)
        {
            int resultado = 0;
            try
            {
                if (modelo != null)
                {
                    _dbSet.Update(modelo);
                    resultado = await _context.SaveChangesAsync();
                }
                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Registrar(T modelo)
        {
            try
            {
                await _dbSet.AddAsync(modelo);
                await _context.SaveChangesAsync();
                // Buscar la propiedad que comience con "id"
                var idProp = typeof(T).GetProperties().FirstOrDefault(p => p.Name.StartsWith("id", StringComparison.OrdinalIgnoreCase) && 
                                      (p.PropertyType == typeof(int) || p.PropertyType == typeof(long)));

                if (idProp != null)
                {
                    var value = idProp.GetValue(modelo);
                    return Convert.ToInt32(value);
                }

                throw new InvalidOperationException("No se encontró una propiedad de ID válida.");

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<T?> ObtenerUnoAsync(Expression<Func<T, bool>> predicado)
        {
            return await _dbSet.FirstOrDefaultAsync(predicado);
        }

    }
}
