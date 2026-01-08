using FirstMariaShopMk1.Context;
using Microsoft.EntityFrameworkCore;

namespace FirstMariaShopMk1.Infrastructure.Repositories
{
    public class Repository<T> :IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity) {
            if (entity is null) { 
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Set<T>().Add(entity);
        }

        public async Task<bool> ExistsAsync(Guid id) {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity is not null;
        }

        public async Task<T?> GetByIdAsync(Guid id) {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity is null) {
                return null;
            }
            return entity;
        }

        public void Remove(T entity) {
            if (entity is null) {
                throw new ArgumentNullException(nameof(entity));
            }
            _context?.Set<T>().Remove(entity);
        }

        public void Update(T entity) {
            if (entity is null) {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
