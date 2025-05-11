using CemSys.Models;

namespace CemSys.Interface.Panteones
{
    public interface IPanteonesBusiness
    {
        Task CrearPanteones(SeccionesPanteone modelo);
        Task<SeccionesPanteone> ConsultarSeccion(int id);
        Task<List<Panteone>> ListaPanteones(int id);
    }
}
