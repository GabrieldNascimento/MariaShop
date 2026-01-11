namespace FirstMariaShopMk1.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}
