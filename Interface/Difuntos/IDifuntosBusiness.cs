using CemSys.Models;
using CemSys.Models.DTO;

namespace CemSys.Interface.Difuntos
{
    public interface IDifuntosBusiness
    {
        Task<int> RegistrarDifunto(Difunto modelo);
        Task<List<EstadoDifunto>> EmitirListadoEstadoDifunto();
        Task<List<DTO_seccionesNicho>> EmitirListadoSeccionesNicho();
        Task<List<DTO_seccionesFosa>> EmitirListadoSeccionesFosa();
        Task<List<DTO_nichos>> EmitirListadoNichos();
        Task<List<DTO_fosas>> EmitirListadoFosas();
        Task<int> RegistrarActaDefuncion(ActaDefuncion modeloacta);

        Task<int> RegistrarEnNicho(NichosDifunto modelo);
        Task<int> RegistrarEnFosa(FosasDifunto modelo);
        Task<int> RegistrarEnPanteon(PanteonDifunto modelo);

        Task<int> IncrementarDifuntoEnNicho(Nicho modelo);
        Task<int> IncrementarDifuntoEnFosa(Nicho modelo);
        Task<int> IncrementarDifuntoEnPanteon(Nicho modelo);

        Task<Nicho> ConsultarNicho(int id);


    }
}
