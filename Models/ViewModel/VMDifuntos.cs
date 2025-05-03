using CemSys.Models.DTO;

namespace CemSys.Models.ViewModel
{
    public class VMDifuntos
    {
        public ABMRepositoryVM<Difunto>? ABMRepositoryVM { get; set; } = new ABMRepositoryVM<Difunto>();
        public List<EstadoDifunto> ListaEstadoDifunto { get; set; } = new List<EstadoDifunto>();
        public List<DTO_seccionesFosa> ListaSeccionesFosa { get; set; } = new List<DTO_seccionesFosa>();
        public List<DTO_seccionesNicho> ListaSeccionesNicho { get; set; } = new List<DTO_seccionesNicho>();
        public List<DTO_nichos> ListaNichos { get; set; } = new List<DTO_nichos>();
        public List<DTO_fosas> ListaFosas { get; set; } = new List<DTO_fosas>();
        public ActaDefuncion actaDefuncion { get; set; } = new ActaDefuncion();

    }
}
