using FirstMariaShopMk1.Application.Pagination;
using FirstMariaShopMk1.Models;

namespace FirstMariaShopMk1.Infrastructure.Repositories
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
