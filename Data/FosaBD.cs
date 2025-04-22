using CemSys.Interface.Fosas;
using CemSys.Models;
using Microsoft.EntityFrameworkCore;


namespace CemSys.Data
{
    public class FosaBD : IFosaDB
    {
        private readonly AppDbContext _context;
        public FosaBD(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SeccionesFosa> ObtenerSeccionFosaPorNombre(string nombre)
        {
            SeccionesFosa modelo = await _context.SeccionesFosas.FirstOrDefaultAsync(opc => opc.Nombre == nombre);
            return modelo;
        }
    }
}
