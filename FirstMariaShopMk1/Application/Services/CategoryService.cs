using AutoMapper;
using MariaShop.Api.Exceptions.InvalidData;
using MariaShop.Api.Exceptions.InvalidUse;
using MariaShop.Api.Application.Pagination;
using MariaShop.Api.Models;
using MariaShop.Api.Application.DTOs;
using MariaShop.Api.Infrastructure.Repositories;
using MariaShop.Api.Infrastructure.UnitOfWork;

namespace MariaShop.Api.Application.Services
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> CreateAsync(CategoryDTO dto) {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var exists = await _categoryRepository.NameExistsAsync(dto.Name);
            if (exists)
                throw new CategoryAlreadyExistsException(dto.Name);

            var category = new Category(
                dto.Name,
                dto.Description);

            _categoryRepository.Add(category);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CategoryDTO>(category);
        }

    
        public async Task UpdateAsync(Guid categoryId, CategoryDTO dto) {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var category = await GetCategoryOrThrowAsync(categoryId);

            if (!string.IsNullOrWhiteSpace(dto.Name))
                category.SetName(dto.Name);

            if (dto.Description != null)
                category.SetDescription(dto.Description);


            await _unitOfWork.CommitAsync();
        }

        public async Task ActivateAsync(Guid categoryId) {
            var category = await GetCategoryOrThrowAsync(categoryId);

            category.Activate();
            await _unitOfWork.CommitAsync();
        }

        public async Task DeactivateAsync(Guid categoryId) {
            var category = await GetCategoryOrThrowAsync(categoryId);

            category.Deactivate();
            await _unitOfWork.CommitAsync();
        }

        public async Task<CategoryDTO?> GetByIdAsync(Guid id) {
            var category = await _categoryRepository.GetByIdAsync(id);
            return (category is null) ? null : _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO?> GetByNameAsync(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));

            var category = await _categoryRepository.GetByNameAsync(name);
            return category is null
                ? null
                : _mapper.Map<CategoryDTO>(category);
        }

        public async Task<PagedResult<CategoryDTO>> GetActiveAsync(PageRequest pagination) {
            ArgumentNullException.ThrowIfNull(pagination);

            var result = await _categoryRepository.GetActiveAsync(pagination);
            return MapPagedResult(result);
        }

        public async Task<PagedResult<CategoryDTO>> GetAllAsync(PageRequest pagination) {
            ArgumentNullException.ThrowIfNull(pagination);

            var result = await _categoryRepository.GetAllAsync(pagination);
            return MapPagedResult(result);
        }

        // =========================
        // Helpers
        // =========================

        private async Task<Category> GetCategoryOrThrowAsync(Guid categoryId) {
            return await _categoryRepository.GetByIdAsync(categoryId)
                ?? throw new CategoryNotFoundException(categoryId);
        }

        private PagedResult<CategoryDTO> MapPagedResult(PagedResult<Category> result) {
            var items = result.Items
                .Select(c => _mapper.Map<CategoryDTO>(c))
                .ToList();

            return PagedResult<CategoryDTO>.Create(
                items,
                result.Page,
                result.PageSize,
                result.TotalItems);
        }

    }

}
