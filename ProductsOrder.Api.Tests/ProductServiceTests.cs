using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using ProductsOrder.Api.Models;
using ProductsOrder.Api.Repositories;
using ProductsOrder.Api.Services;

namespace ProductsOrder.Api.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetProductsAsync_ReturnAllProducts()
        {
            // Arrange
            var mockProducts = new List<Product>
            {
                new() { Id = 1, Name = "Test Product 1", Price = 10},
                new() { Id = 2, Name = "Test Product 2", Price = 20}
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockProducts);

            var mockLogger = new Mock<ILogger<ProductService>>();

            var service = new ProductService(mockRepo.Object, mockLogger.Object);

            // Act
            var result = await service.GetProductsAsync();
            var resultsList = result.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockProducts.Count, resultsList.Count);
            Assert.Equal(mockProducts[0].Name, resultsList[0].Name);
        }
    }
}