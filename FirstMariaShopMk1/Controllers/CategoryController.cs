using MariaShop.Api.Application.DTOs;
using MariaShop.Api.Application.Pagination;
using MariaShop.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MariaShop.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public sealed class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryService categoryService,
            ILogger<CategoryController> logger) {
            _categoryService = categoryService;
            _logger = logger;
        }

        // =========================
        // CREATE
        // =========================

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create(
            [FromBody] CategoryDTO dto) {
            _logger.LogInformation("Creating category with name {Name}", dto?.Name);

            var result = await _categoryService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetByName),
                new { name = result.Name },
                result);
        }

        // =========================
        // UPDATE
        // =========================

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] CategoryDTO dto) {
            _logger.LogInformation("Updating category {CategoryId}", id);

            await _categoryService.UpdateAsync(id, dto);
            return NoContent();
        }

        // =========================
        // STATE
        // =========================

        [HttpPatch("{id:guid}/activate")]
        public async Task<IActionResult> Activate(Guid id) {
            _logger.LogInformation("Activating category {CategoryId}", id);

            await _categoryService.ActivateAsync(id);
            return NoContent();
        }

        [HttpPatch("{id:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id) {
            _logger.LogInformation("Deactivating category {CategoryId}", id);

            await _categoryService.DeactivateAsync(id);
            return NoContent();
        }

        // =========================
        // READ
        // =========================

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<CategoryDTO>> GetByName(string name) {
            var result = await _categoryService.GetByNameAsync(name);

            return result is null
                ? NotFound()
                : Ok(result);
        }

        [HttpGet("active")]
        public async Task<ActionResult<PagedResult<CategoryDTO>>> GetActive(
            [FromQuery] PageRequest pagination) {
            var result = await _categoryService.GetActiveAsync(pagination);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<CategoryDTO>>> GetAll(
            [FromQuery] PageRequest pagination) {
            var result = await _categoryService.GetAllAsync(pagination);
            return Ok(result);
        }
    }
}
