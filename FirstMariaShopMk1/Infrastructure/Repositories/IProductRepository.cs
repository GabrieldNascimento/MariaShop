using MariaShop.Api.Application.Pagination;
using MariaShop.Api.Models;

namespace MariaShop.Api.Infrastructure.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedResult<Product>> GetActiveAsync(
            PageRequest pagination);

        Task<PagedResult<Product>> GetByCategoryAsync(
            Guid categoryId,
            PageRequest pagination);

        Task<PagedResult<Product>> GetWithLowStockAsync(
            int threshold,
            PageRequest pagination);

        Task<bool> ExistsWithNameInCategoryAsync(
            string name,
            Guid categoryId);
    }

}
