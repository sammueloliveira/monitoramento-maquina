namespace MonitoramentoMaquinas.Domain.Interfaces
{
    public interface IGeneric<T> where T : class
    {
        Task Add(T objeto);
        Task Update(T objeto);
        Task Delete(T objeto);
        Task<T> GetEntityById(Guid id);
        Task<List<T>> List();
    }
}
