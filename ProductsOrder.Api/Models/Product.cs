using System.ComponentModel.DataAnnotations;

namespace ProductsOrder.Api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }

    public record CreateProductDto(
        [Required][StringLength(100)] string Name,
        [Range(0.01, 10000)] decimal Price
    );

    public record UpdateProductDto(
        [Required][StringLength(100)] string Name,
        [Range(0.01, 10000)] decimal Price
    );
}
