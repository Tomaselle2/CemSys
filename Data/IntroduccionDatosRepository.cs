using CemSys.Interface;
using CemSys.Models;
using Microsoft.EntityFrameworkCore;

namespace CemSys.Data
{
    public class IntroduccionDatosRepository : IIntroducion_datos
    {
        private readonly AppDbContext _context;
        public IntroduccionDatosRepository(AppDbContext contex)
        {
            _context = contex;
        }

        public async Task<Difunto> ConsultarDifunto(int id)
        {
            try
            {
                Difunto difunto = await _context.Difuntos
                        .Include(acta => acta.ActaDefuncionNavigation)
                        .Include(est => est.EstadoNavigation)
                        .Where(d => d.IdDifunto == id)
                        .FirstOrDefaultAsync();
                return difunto;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<FosasDifunto>> EmitirListadoFosasDifuntos()
        {
            try
            {
                List<FosasDifunto> lista = await _context.FosasDifuntos.Include(d => d.Difunto).ThenInclude(e => e.EstadoNavigation).Include(n => n.Fosa).ThenInclude(s => s.SeccionNavigation).OrderBy(secc => secc.Fosa.Seccion).ThenBy(fosa => fosa.Fosa.NroFosa).ToListAsync();
                return lista;
            }
            catch (Exception) { throw; }
            
        }

        public async Task<List<NichosDifunto>> EmitirListadoNichosDifuntos()
        {
            try
            {
                List<NichosDifunto> lista = await _context.NichosDifuntos.Include(d => d.Difunto).ThenInclude(e => e.EstadoNavigation).Include(n => n.Nicho).ThenInclude(s => s.SeccionNavigation).OrderBy(secc=>secc.Nicho.Seccion).ThenBy(ni => ni.Nicho.NroNicho).ToListAsync();
                return lista;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<PanteonDifunto>> EmitirListadoPanteonDifuntos()
        {
            try
            {
                List<PanteonDifunto> lista = await _context.PanteonDifuntos.Include(d => d.Difunto).ThenInclude(e => e.EstadoNavigation).Include(n => n.Panteon).ThenInclude(s => s.IdSeccionPanteonNavigation).OrderBy(secc=>secc.Panteon.IdSeccionPanteon).ThenBy(lote => lote.Panteon.NroLote).ToListAsync();
                return lista;
            }
            catch (Exception) { throw; }
        }
    }
}
