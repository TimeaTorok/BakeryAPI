using BakeryManagement.Application.Constants;
using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.DTOs.Products;
using BakeryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BakeryManagement.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetAll(
            [FromQuery, Range(PaginationLimits.MinimumPageNumber, PaginationLimits.MaximumPageNumber)]
                int pageNumber = PaginationLimits.DefaultPageNumber,
            [FromQuery, Range(PaginationLimits.MinimumPageSize, PaginationLimits.MaximumPageSize)]
                int pageSize = PaginationLimits.DefaultPageSize,
            [FromQuery] string? category = null,
            [FromQuery] bool? isAvailable = null) =>
            Ok(await productService.GetAllProductsAsync(pageNumber, pageSize, category, isAvailable));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> Create(CreateProductDto createProduct)
        {
            var product = await productService.CreateProductAsync(createProduct);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> Update(
            Guid id,
            UpdateProductDto updateProduct)
        {
            var product = await productService.UpdateProductAsync(id, updateProduct);
            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
