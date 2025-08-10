using Microsoft.EntityFrameworkCore;
using ProductsOrder.Api.Data;
using ProductsOrder.Api.Models;

namespace ProductsOrder.Api.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllAsync() => await context.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) => await context.Products.FindAsync(id);

        public async Task<Product> AddAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            context.Products.Update(product);            
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> NameExistsAsync(string name, int? excludeId = null)
        {
            var query = context.Products.Where(p => p.Name.ToLower() == name.ToLower());

            if (excludeId.HasValue)
                query = query.Where(p => p.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
}
