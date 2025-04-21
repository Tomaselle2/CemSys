namespace CemSys.Models.ViewModel
{
    public class VMNichos
    {
        public ABMRepositoryVM<Nicho>? ABMRepositoryVM { get; set; } = new ABMRepositoryVM<Nicho>();
        public SeccionesNicho? seccion { get; set; }
        public List<TipoNicho>? ListaTipoNicho { get; set; }
    }
}
