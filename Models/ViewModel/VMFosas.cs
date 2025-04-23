namespace CemSys.Models.ViewModel
{
    public class VMFosas
    {
        public ABMRepositoryVM<Fosa>? ABMRepositoryVM { get; set; } = new ABMRepositoryVM<Fosa>();
        public SeccionesFosa? seccion { get; set; }
    }
}
