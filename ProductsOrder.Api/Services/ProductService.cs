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

        public Task<Product> CreateProductAsync(CreateProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(int id, UpdateProductDto productDto)
        {
            throw new NotImplementedException();
        }
    }
}
