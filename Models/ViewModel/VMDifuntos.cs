namespace CemSys.Models.ViewModel
{
    public class VMDifuntos
    {
        public ABMRepositoryVM<Difunto>? ABMRepositoryVM { get; set; } = new ABMRepositoryVM<Difunto>();
        public List<EstadoDifunto> ListaEstadoDifunto { get; set; } = new List<EstadoDifunto>();
        public List<SeccionesFosa> ListaSeccionesFosa { get; set; } = new List<SeccionesFosa>();
        public List<SeccionesNicho> ListaSeccionesNicho { get; set; } = new List<SeccionesNicho>();
        public List<Nicho> ListaNichos { get; set; } = new List<Nicho>();
        public List<Fosa> ListaFosas { get; set; } = new List<Fosa>();
        public ActaDefuncion actaDefuncion { get; set; } = new ActaDefuncion();

    }
}
