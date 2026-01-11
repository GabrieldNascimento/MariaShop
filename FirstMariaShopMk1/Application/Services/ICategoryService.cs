using FirstMariaShopMk1.Application.DTOs;
using FirstMariaShopMk1.Application.Pagination;

namespace FirstMariaShopMk1.Application.Services
{
    public interface ICategoryService
    {
        Task<CategoryDTO> CreateAsync(CategoryDTO dto);

        Task UpdateAsync(Guid categoryId, CategoryDTO dto);

        Task ActivateAsync(Guid categoryId);
        Task DeactivateAsync(Guid categoryId);

        Task<CategoryDTO?> GetByNameAsync(string name);

        Task<PagedResult<CategoryDTO>> GetActiveAsync(PageRequest pagination);
        Task<PagedResult<CategoryDTO>> GetAllAsync(PageRequest pagination);
    }
}
