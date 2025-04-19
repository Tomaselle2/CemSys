using CemSys.Models;

namespace CemSys.Interface.SeccionesNichos
{
    public interface ISeccionesNichosBusiness
    {
        Task CrearNichosNumeracionNueva();
        Task CrearNichosNumeracionAntigua();
        Task<List<TipoNumeracionParcela>> ListaTipoNumeracionParcela();

    }
}
