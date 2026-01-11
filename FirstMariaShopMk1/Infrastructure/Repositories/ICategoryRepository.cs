using FirstMariaShopMk1.Application.Pagination;
using FirstMariaShopMk1.Models;

namespace FirstMariaShopMk1.Infrastructure.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetByNameAsync(string name);

        Task<bool> NameExistsAsync(string name);

        Task<PagedResult<Category>> GetActiveAsync(
            PageRequest pagination);

        Task<PagedResult<Category>> GetAllAsync(
            PageRequest pagination);
    }

}
