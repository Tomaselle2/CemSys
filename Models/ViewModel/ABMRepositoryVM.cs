namespace CemSys.Models.ViewModel
{
    public class ABMRepositoryVM<T> where T : class
    {
        public List<T> Lista { get; set; } = new List<T>();
        public T? Modelo{ get; set; }
        public bool EsModificacion { get; set; } = false;
        
    }
}
