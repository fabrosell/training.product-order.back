using ProductsOrder.Api.Models;
using ProductsOrder.Api.Repositories;

namespace ProductsOrder.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            this._productRepository = productRepository;
            this._logger = logger;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            _logger.LogInformation("Fetching all products from the service layer");

            return await this._productRepository.GetAllAsync();
        }
    }
}
