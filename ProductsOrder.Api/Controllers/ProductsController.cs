using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get()
        {
            var products = await this._productService.GetProductsAsync();
            return Ok(products);
        }
    }
}
