using AutoMapper;
using MariaShop.Api.Exceptions.InvalidData;
using MariaShop.Api.Exceptions.InvalidUse;
using MariaShop.Api.Application.Pagination;
using MariaShop.Api.Models;
using MariaShop.Api.Infrastructure.Repositories;
using MariaShop.Api.Application.DTOs;
using MariaShop.Api.Infrastructure.UnitOfWork;

namespace MariaShop.Api.Application.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDTO> CreateAsync(ProductDTO dto) {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var exists = await _productRepository
                .ExistsWithNameInCategoryAsync(dto.Name, dto.CategoryId);

            if (exists)
                throw new ProductAlreadyExistsException(dto.Name);

            var product = new Product(
                dto.Name,
                dto.Price,
                dto.StockQuantity,
                dto.CategoryId,
                dto.Description);

            _productRepository.Add(product);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task UpdateAsync(Guid productId, ProductDTO dto) {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var product = await GetProductOrThrow(productId);

            if (!string.IsNullOrWhiteSpace(dto.Name))
                product.SetName(dto.Name);

            if (dto.Price > 0)
                product.SetPrice(dto.Price);

            if (dto.Description is not null)
                product.SetDescription(dto.Description);

            await _unitOfWork.CommitAsync();
        }

        public async Task IncreaseStockAsync(Guid productId, int quantity) {
            var product = await GetProductOrThrow(productId);

            product.IncreaseStock(quantity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DecreaseStockAsync(Guid productId, int quantity) {
            var product = await GetProductOrThrow(productId);

            product.DecreaseStock(quantity);
            await _unitOfWork.CommitAsync();
        }

        public async Task ActivateAsync(Guid productId) {
            var product = await GetProductOrThrow(productId);

            product.Activate();
            await _unitOfWork.CommitAsync();
        }

        public async Task DeactivateAsync(Guid productId) {
            var product = await GetProductOrThrow(productId);

            product.Deactivate();
            await _unitOfWork.CommitAsync();
        }

        public async Task<PagedResult<ProductDTO>> GetActiveAsync(PageRequest pagination) {
            ArgumentNullException.ThrowIfNull(pagination);

            var result = await _productRepository.GetActiveAsync(pagination);
            return MapPagedResult(result);
        }

        public async Task<ProductDTO?> GetByIdAsync(Guid productId) {
            var product = await _productRepository.GetByIdAsync(productId);
            return (product is null) ? null: _mapper.Map<ProductDTO>(product);
        }

        public async Task<PagedResult<ProductDTO>> GetByCategoryAsync(
            Guid categoryId,
            PageRequest pagination) {
            ArgumentNullException.ThrowIfNull(pagination);

            var result = await _productRepository.GetByCategoryAsync(
                categoryId,
                pagination);

            return MapPagedResult(result);
        }

        public async Task<PagedResult<ProductDTO>> GetWithLowStockAsync(
            int threshold,
            PageRequest pagination) {
            ArgumentNullException.ThrowIfNull(pagination);

            var result = await _productRepository.GetWithLowStockAsync(
                threshold,
                pagination);

            return MapPagedResult(result);
        }

        // =========================
        // Helpers
        // =========================

        private async Task<Product> GetProductOrThrow(Guid productId) {
            return await _productRepository.GetByIdAsync(productId)
                ?? throw new ProductNotFoundException(productId);
        }

        private PagedResult<ProductDTO> MapPagedResult(PagedResult<Product> result) {
            var items = result.Items
                .Select(p => _mapper.Map<ProductDTO>(p))
                .ToList();

            return PagedResult<ProductDTO>.Create(
                items,
                result.Page,
                result.PageSize,
                result.TotalItems);
        }
    }
}
