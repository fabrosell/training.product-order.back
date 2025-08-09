using ProductsOrder.Api.Models;

namespace ProductsOrder.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
