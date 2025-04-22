using CemSys.Models;

namespace CemSys.Interface.Fosas
{
    public interface IFosasBusiness
    {
        Task CrearFosas(SeccionesFosa modelo);
        Task<SeccionesFosa> ObtenerSeccionFosaPorNombre(string nombre);
        Task<SeccionesFosa> ConsultarSeccionFosa(int id);
        Task<List<Fosa>> ListaFosas();

    }
}
