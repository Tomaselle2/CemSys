using CemSys.Models;

namespace CemSys.Interface.SeccionesNichos
{
    public interface ISeccionesNichosBusiness
    {

        Task<List<TipoNumeracionParcela>> ListaTipoNumeracionParcela();

    }
}
