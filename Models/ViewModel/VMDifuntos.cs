using CemSys.Models.DTO;

namespace CemSys.Models.ViewModel
{
    public class VMDifuntos
    {
        public ABMRepositoryVM<Difunto>? ABMRepositoryVM { get; set; } = new ABMRepositoryVM<Difunto>();
        public List<EstadoDifunto> ListaEstadoDifunto { get; set; } = new List<EstadoDifunto>();
        public List<DTO_seccionesFosa> ListaSeccionesFosa { get; set; } = new List<DTO_seccionesFosa>();
        public List<DTO_seccionesNicho> ListaSeccionesNicho { get; set; } = new List<DTO_seccionesNicho>();
        public List<DTO_seccionesPanteon> ListaSeccionesPanteon { get; set; } = new List<DTO_seccionesPanteon>();

        public List<DTO_nichos> ListaNichos { get; set; } = new List<DTO_nichos>();
        public List<DTO_fosas> ListaFosas { get; set; } = new List<DTO_fosas>();
        public List<DTO_panteones> ListaPanteones { get; set; } = new List<DTO_panteones>();

        public List<DTO_difunto> ListaDifuntos { get; set; } = new List<DTO_difunto>();
        public ActaDefuncion actaDefuncion { get; set; } = new ActaDefuncion();
        public DateOnly fechaActual { get; set; }

    }
}
