using CemSys.Models;

namespace CemSys.Interface.Nichos
{
    public interface INichosDB
    {
        Task<SeccionesNicho> ObtenerSeccionNichoPorNombre(string nombre);
    }
}
