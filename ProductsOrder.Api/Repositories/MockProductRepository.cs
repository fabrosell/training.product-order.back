using ProductsOrder.Api.Models;

namespace ProductsOrder.Api.Repositories
{
    public class MockProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product> {
            new() { Id = 1, Name = "Laptop Pro", Price = 1200.50m },
            new() { Id = 2, Name = "Wireless Mouse", Price = 75.00m },
            new() { Id = 3, Name = "Mechanical Keyboard", Price = 150.75m }
        };

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Product>>(_products);
        }
    }
}
