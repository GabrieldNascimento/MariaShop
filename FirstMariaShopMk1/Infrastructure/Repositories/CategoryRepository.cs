using FirstMariaShopMk1.Application.Pagination;
using FirstMariaShopMk1.Context;
using FirstMariaShopMk1.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMariaShopMk1.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext){ 
        }

        public async Task<Category?> GetByNameAsync(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));

            var result = await _appDbContext.Categories
                .FirstOrDefaultAsync(c => c.Name == name);

            return result;
        }

        public async Task<bool> NameExistsAsync(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
            return await _appDbContext.Categories.AnyAsync(c => c.Name == name);

        }

       public async Task<PagedResult<Category>> GetActiveAsync(PageRequest pagination) {
            ArgumentNullException.ThrowIfNull(pagination);

            var query =  _appDbContext.Categories.Where(c => c.IsActive);

            var totalItems = await query.CountAsync();

            var items = await query.Skip(pagination.Offset).Take(pagination.PageSize).ToListAsync();

            return PagedResult<Category>.Create(
                    items,
                    pagination.Page,
                    pagination.PageSize,
                    totalItems
            );
       
       }

        public async Task<PagedResult<Category>> GetAllAsync(PageRequest pagination) {
            ArgumentNullException.ThrowIfNull(pagination);

            var query = _appDbContext.Categories;

            var totalItems = await query.CountAsync();

            var items = await query.Skip(pagination.Offset).Take(pagination.PageSize).ToListAsync();

            return PagedResult<Category>.Create(
                    items,
                    pagination.Page,
                    pagination.PageSize,
                    totalItems
                );

        }

    }
}
