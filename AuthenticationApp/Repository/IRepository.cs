namespace AuthenticationApp.Repository
{
    public interface IRepository<T>
    {        
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetElementAsync(int id);
        Task SaveChangesAsync();
    }
}
