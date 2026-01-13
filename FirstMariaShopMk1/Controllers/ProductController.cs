using MariaShop.Api.Application.DTOs;
using MariaShop.Api.Application.Pagination;
using MariaShop.Api.Application.Services;

namespace MariaShop.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/products")]
    public sealed class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IProductService productService,
            ILogger<ProductController> logger) {
            _productService = productService;
            _logger = logger;
        }

        // =========================
        // CREATE
        // =========================

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(
            [FromBody] ProductDTO dto) {
            _logger.LogInformation(
                "Creating product {Name} in category {CategoryId}",
                dto.Name,
                dto.CategoryId);

            var result = await _productService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        // =========================
        // READ
        // =========================

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDTO>> GetById(Guid id) {
            var result = await _productService.GetByIdAsync(id);

            return result is null
                ? NotFound()
                : Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductDTO>>> GetAll(
            [FromQuery] PageRequest pagination) {
            var result = await _productService.GetActiveAsync(pagination);
            return Ok(result);
        }

        [HttpGet("by-category/{categoryId:guid}")]
        public async Task<ActionResult<PagedResult<ProductDTO>>> GetByCategory(
            Guid categoryId,
            [FromQuery] PageRequest pagination) {
            var result = await _productService.GetByCategoryAsync(
                categoryId,
                pagination);

            return Ok(result);
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<PagedResult<ProductDTO>>> GetWithLowStock(
            [FromQuery] int threshold,
            [FromQuery] PageRequest pagination) {
            var result = await _productService.GetWithLowStockAsync(
                threshold,
                pagination);

            return Ok(result);
        }

        // =========================
        // UPDATE
        // =========================

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] ProductDTO dto) {
            _logger.LogInformation(
                "Updating product {ProductId}",
                id);

            await _productService.UpdateAsync(id, dto);
            return NoContent();
        }

        // =========================
        // STATE
        // =========================

        [HttpPatch("{id:guid}/activate")]
        public async Task<IActionResult> Activate(Guid id) {
            _logger.LogInformation(
                "Activating product {ProductId}",
                id);

            await _productService.ActivateAsync(id);
            return NoContent();
        }

        [HttpPatch("{id:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id) {
            _logger.LogInformation(
                "Deactivating product {ProductId}",
                id);

            await _productService.DeactivateAsync(id);
            return NoContent();
        }

        // =========================
        // STOCK
        // =========================

        [HttpPatch("{id:guid}/increase-stock")]
        public async Task<IActionResult> IncreaseStock(
            Guid id,
            [FromBody] int quantity) {
            _logger.LogInformation(
                "Increasing stock of product {ProductId} by {Quantity}",
                id,
                quantity);

            await _productService.IncreaseStockAsync(id, quantity);
            return NoContent();
        }

        [HttpPatch("{id:guid}/decrease-stock")]
        public async Task<IActionResult> DecreaseStock(
            Guid id,
            [FromBody] int quantity) {
            _logger.LogInformation(
                "Decreasing stock of product {ProductId} by {Quantity}",
                id,
                quantity);

            await _productService.DecreaseStockAsync(id, quantity);
            return NoContent();
        }
    }

}
