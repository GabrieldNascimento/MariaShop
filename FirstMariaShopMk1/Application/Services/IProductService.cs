
using FirstMariaShopMk1.Application.DTOs;
using FirstMariaShopMk1.Application.Pagination;

namespace FirstMariaShopMk1.Application.Services
{
    public interface IProductService
    {
        Task<ProductDTO> CreateAsync(ProductDTO dto);
        Task UpdateAsync(Guid productId, ProductDTO dto);


        Task IncreaseStockAsync(Guid productId, int quantity);
        Task DecreaseStockAsync(Guid productId, int quantity);

        Task ActivateAsync(Guid productId);
        Task DeactivateAsync(Guid productId);

        Task<PagedResult<ProductDTO>> GetActiveAsync(PageRequest pagination);
        Task<PagedResult<ProductDTO>> GetByCategoryAsync(
            Guid categoryId,
            PageRequest pagination);

        Task<PagedResult<ProductDTO>> GetWithLowStockAsync(
            int threshold,
            PageRequest pagination);
    }


}
