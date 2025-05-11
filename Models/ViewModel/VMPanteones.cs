namespace CemSys.Models.ViewModel
{
    public class VMPanteones
    {
        public ABMRepositoryVM<Panteone>? ABMRepositoryVM { get; set; } = new ABMRepositoryVM<Panteone>();
        public SeccionesPanteone? seccion { get; set; }
    }
}
