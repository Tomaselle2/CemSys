namespace CemSys.Models.ViewModel
{
    public class VMUsuarios
    {
        public ABMRepositoryVM<Usuario>? ABMRepositoryVM { get; set; } = new ABMRepositoryVM<Usuario>();
        public List<TipoUsuario> ListaTipoUsuario { get; set; } = new List<TipoUsuario>();
    }
}
