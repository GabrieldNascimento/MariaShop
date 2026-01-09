using FirstMariaShopMk1.Application.Pagination;
using FirstMariaShopMk1.Context;
using FirstMariaShopMk1.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMariaShopMk1.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            
        }


        public async Task<PagedResult<Product>> GetActiveAsync(Pagination pagination) {
            ArgumentNullException.ThrowIfNull(pagination);

            var query = _context.Products
                .Where(p => p.IsActive);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip(pagination.Offset)
                .Take(pagination.PageSize)
                .ToListAsync();

            return PagedResult<Product>.Create(
                items,
                pagination.Page,
                pagination.PageSize,
                totalItems);
        }

        public async Task<bool> ExistsWithNameInCategoryAsync(string name, Guid categoryId) {
            
            if (String.IsNullOrWhiteSpace(name)){
                throw new ArgumentException(
                "Name cannot be null or empty.",
                nameof(name));
            }

            return await _context.Products.AnyAsync(p => p.Name == name && p.CategoryId == categoryId);
        }


        public async Task<PagedResult<Product>> GetByCategoryAsync(
        Guid categoryId,
        Pagination pagination) {
            ArgumentNullException.ThrowIfNull(pagination);

            var query = _context.Products
                .Where(p => p.CategoryId == categoryId);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip(pagination.Offset)
                .Take(pagination.PageSize)
                .ToListAsync();

            return PagedResult<Product>.Create(
                items,
                pagination.Page,
                pagination.PageSize,
                totalItems
            );
        }



        public async Task<PagedResult<Product>> GetWithLowStockAsync(
        int threshold,
        Pagination pagination) {
            if (threshold < 0)
                throw new ArgumentOutOfRangeException(nameof(threshold));

            ArgumentNullException.ThrowIfNull(pagination);

            var query = _context.Products
                .Where(p => p.StockQuantity <= threshold);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip(pagination.Offset)
                .Take(pagination.PageSize)
                .ToListAsync();

            return PagedResult<Product>.Create(
                items,
                pagination.Page,
                pagination.PageSize,
                totalItems
            );
        }
    }
}
