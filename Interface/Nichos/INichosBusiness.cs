using CemSys.Models;

namespace CemSys.Interface.Nichos
{
    public interface INichosBusiness
    {
        Task CrearNichosNumeracionNueva(SeccionesNicho modelo);
        Task CrearNichosNumeracionAntigua(SeccionesNicho modelo);

        Task<SeccionesNicho> ObtenerSeccionNicho(int id);
        Task<SeccionesNicho> ObtenerSeccionNichoPorNombre(string nombre);
        Task<List<TipoNicho>> ListaTipoNicho();
        Task<List<Nicho>> ListaDeNichos(int id);
    }
}
