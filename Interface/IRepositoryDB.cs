namespace CemSys.Interface
{
    public interface IRepositoryDB<T> where T : class
    {
        Task<int> Registrar(T modelo);
        Task<List<T>> EmitirListado();
        Task<T> Consultar(int id);
        Task<int> Modificar(T modelo);
        Task Eliminar(int id);
    }
}
