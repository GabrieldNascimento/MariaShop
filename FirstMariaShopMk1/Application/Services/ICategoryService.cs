using MariaShop.Api.Application.DTOs;
using MariaShop.Api.Application.Pagination;

namespace MariaShop.Api.Application.Services
{
    public interface ICategoryService
    {
        Task<CategoryDTO> CreateAsync(CategoryDTO dto);

        Task UpdateAsync(Guid categoryId, CategoryDTO dto);

        Task ActivateAsync(Guid categoryId);
        Task DeactivateAsync(Guid categoryId);


        Task<CategoryDTO?> GetByIdAsync(Guid id);

        Task<CategoryDTO?> GetByNameAsync(string name);

        Task<PagedResult<CategoryDTO>> GetActiveAsync(PageRequest pagination);
        Task<PagedResult<CategoryDTO>> GetAllAsync(PageRequest pagination);
    }
}
