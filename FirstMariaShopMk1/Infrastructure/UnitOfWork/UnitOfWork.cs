using MariaShop.Api.Context;

namespace MariaShop.Api.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task CommitAsync() {
            await _context.SaveChangesAsync();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}
