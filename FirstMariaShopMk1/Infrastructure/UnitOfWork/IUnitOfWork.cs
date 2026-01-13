namespace MariaShop.Api.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}
