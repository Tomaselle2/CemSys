using CemSys.Models.DTO;

namespace CemSys.Models.ViewModel
{
    public class VM_Introduccion_Listado
    {
        public List<NichosDifunto> ListaNichosDifuntos { get; set; } = new List<NichosDifunto>();
        public List<FosasDifunto> ListaFosasDifuntos { get; set; } = new List<FosasDifunto>();
        public List<PanteonDifunto> ListaPanteonDifuntos { get; set; } = new List<PanteonDifunto>();
        public List<DTO_seccionesFosa> ListaSeccionesFosas { get; set; } = new List<DTO_seccionesFosa>();
        public List<DTO_seccionesNicho> ListaSeccionesNichos { get; set; } = new List<DTO_seccionesNicho>();
        public List<DTO_seccionesPanteon> ListaSeccionesPanteones { get; set; } = new List<DTO_seccionesPanteon>();


    }
}
