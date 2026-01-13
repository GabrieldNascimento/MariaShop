namespace MariaShop.Api.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);

        Task<bool> ExistsAsync(Guid id);

        void Remove(T entity);
        void Update(T entity);
        void Add(T entity);
    }
}
