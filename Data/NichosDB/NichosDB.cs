using CemSys.Interface.Nichos;
using CemSys.Models;
using Microsoft.EntityFrameworkCore;

namespace CemSys.Data.NichosDB
{
    public class NichosDB : INichosDB
    {
        private readonly AppDbContext _context;
        public NichosDB(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SeccionesNicho> ObtenerSeccionNichoPorNombre(string nombre)
        {
            SeccionesNicho modelo = await _context.SeccionesNichos.FirstOrDefaultAsync(opc => opc.Nombre == nombre);
            return modelo;
        }
    }
}
