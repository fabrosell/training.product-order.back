using ProductsOrder.Api.Models;

namespace ProductsOrder.Api.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
