namespace CemSys.Models.ViewModel
{
    public class VMSeccionesNichos
    {
        public ABMRepositoryVM<SeccionesNicho>? ABMRepositoryVM { get; set; } = new ABMRepositoryVM<SeccionesNicho>();
        public List<TipoNumeracionParcela> ListaNumeracionParcelas { get; set; } = new List<TipoNumeracionParcela>();
    }
}
