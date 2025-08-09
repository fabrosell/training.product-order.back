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

        public async  Task<Product> CreateProductAsync(CreateProductDto productDto)
        {
            _logger.LogInformation($"Creating product with name = {productDto.Name}");
            return await this._productRepository.AddAsync(new Product() { Name = productDto.Name, Price = productDto.Price });
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await this._productRepository.GetByIdAsync(id);

            if (product == null) return false;

            await this._productRepository.DeleteAsync(id);

            return true;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching products by id = {id}");
            return await this._productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            _logger.LogInformation("Fetching all products from the service layer");
            return await this._productRepository.GetAllAsync();
        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductDto productDto)
        {
            var product = await this._productRepository.GetByIdAsync(id);
            
            if (product == null) return false;

            product.Name = productDto.Name;
            product.Price = productDto.Price;

            await this._productRepository.UpdateAsync(product);

            return true;
        }
    }
}
