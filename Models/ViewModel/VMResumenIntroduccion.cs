namespace CemSys.Models.ViewModel
{
    public class VMResumenIntroduccion
    {
        public List<TramiteViewModel> ListaTramites { get; set; } = new List<TramiteViewModel>();   
        public DateOnly? fechaDesde { get; set; }
        public DateOnly? fechaHasta { get; set; }
    }
}
