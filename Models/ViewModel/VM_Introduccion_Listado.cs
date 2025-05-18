namespace CemSys.Models.ViewModel
{
    public class VM_Introduccion_Listado
    {
        public List<NichosDifunto> ListaNichosDifuntos { get; set; } = new List<NichosDifunto>();
        public List<FosasDifunto> ListaFosasDifuntos { get; set; } = new List<FosasDifunto>();
        public List<PanteonDifunto> ListaPanteonDifuntos { get; set; } = new List<PanteonDifunto>();

    }
}
