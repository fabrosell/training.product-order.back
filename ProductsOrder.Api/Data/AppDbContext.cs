using Microsoft.EntityFrameworkCore;
using ProductsOrder.Api.Models;

namespace ProductsOrder.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}
