using CemSys.Models;

namespace CemSys.Interface.Difuntos
{
    public interface IDifuntosBusiness
    {
        Task<int> RegistrarDifunto(Difunto modelo);
        Task<List<EstadoDifunto>> EsmitirListadoEstadoDifunto();
        Task<List<SeccionesNicho>> EsmitirListadoSeccionesNicho();
        Task<List<SeccionesFosa>> EsmitirListadoSeccionesFosa();
        Task<List<Nicho>> EsmitirListadoNichos();
        Task<List<Fosa>> EsmitirListadoFosas();
        Task<int> RegistrarActaDefuncion();


    }
}
