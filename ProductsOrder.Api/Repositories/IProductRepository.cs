using ProductsOrder.Api.Models;

namespace ProductsOrder.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<bool> NameExistsAsync(string name, int? excludeId = null);
    }
}
