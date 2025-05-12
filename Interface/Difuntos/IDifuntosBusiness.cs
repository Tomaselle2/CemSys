using CemSys.Models;
using CemSys.Models.DTO;

namespace CemSys.Interface.Difuntos
{
    public interface IDifuntosBusiness
    {
        Task<int> RegistrarDifunto(Difunto modelo);
        Task<int> RegistrarActaDefuncion(ActaDefuncion modeloacta);

        Task<List<EstadoDifunto>> EmitirListadoEstadoDifunto();

        Task<List<DTO_seccionesNicho>> EmitirListadoSeccionesNicho();
        Task<List<DTO_seccionesFosa>> EmitirListadoSeccionesFosa();
        Task<List<DTO_seccionesPanteon>> EmitirListadoSeccionesPanteon();

        Task<List<DTO_nichos>> EmitirListadoNichos();
        Task<List<DTO_fosas>> EmitirListadoFosas();
        Task<List<DTO_panteones>> EmitirListadoPanteones();
        Task<List<DTO_difunto>> EmitirListadoDifuntos();
     

        Task<int> RegistrarEnNicho(NichosDifunto modelo);
        Task<int> RegistrarEnFosa(FosasDifunto modelo);
        Task<int> RegistrarEnPanteon(PanteonDifunto modelo);

        Task<int> IncrementarDifuntoEnNicho(Nicho modelo);
        Task<int> IncrementarDifuntoEnFosa(Fosa modelo);
        Task<int> IncrementarDifuntoEnPanteon(Panteone modelo);

        Task<Nicho> ConsultarNicho(int id);
        Task<Fosa> ConsultarFosa(int id);
        Task<Panteone> ConsultarPanteon(int id);



    }
}
