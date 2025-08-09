using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsOrder.Api.Models;
using ProductsOrder.Api.Services;

namespace ProductsOrder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await this._productService.GetProductsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await this._productService.GetProductByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto productDto)
        {
            var newProduct = await this._productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto productDto)
        {
            var success = await this._productService.UpdateProductAsync(id, productDto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await this._productService.DeleteProductAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
