using CemSys.Models;

namespace CemSys.Interface
{
    public interface IIntroducion_datos
    {
        Task<List<NichosDifunto>> EmitirListadoNichosDifuntos();
        Task<List<FosasDifunto>> EmitirListadoFosasDifuntos();
        Task<List<PanteonDifunto>> EmitirListadoPanteonDifuntos();
        Task<Difunto> ConsultarDifunto(int id);
    }
}
