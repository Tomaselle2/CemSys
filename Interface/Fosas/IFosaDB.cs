using CemSys.Models;

namespace CemSys.Interface.Fosas
{
    public interface IFosaDB
    {
        Task<SeccionesFosa> ObtenerSeccionFosaPorNombre(string nombre);

    }
}
