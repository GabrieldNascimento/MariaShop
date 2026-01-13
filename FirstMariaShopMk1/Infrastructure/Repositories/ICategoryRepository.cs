using MariaShop.Api.Application.Pagination;
using MariaShop.Api.Models;

namespace MariaShop.Api.Infrastructure.Repositories
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
