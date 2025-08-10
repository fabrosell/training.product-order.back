using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using ProductsOrder.Api.Models;
using ProductsOrder.Api.Models.Exceptions;
using ProductsOrder.Api.Repositories;
using ProductsOrder.Api.Services;

namespace ProductsOrder.Api.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly Mock<ILogger<ProductService>> _mockLogger;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger<ProductService>>();
            _service = new ProductService(this._mockRepo.Object, this._mockLogger.Object);
        }


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

        [Fact]
        public async Task CreateProductAsync_ShouldReturnCreatedProduct()
        {
            // Arrange
            var dto = new CreateProductDto("New Gadget", 99.99m);
            var product = new Product { Id = 1, Name = dto.Name, Price = dto.Price };
            _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Product>())).ReturnsAsync(product);

            // Act
            var result = await _service.CreateProductAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("New Gadget", result.Name);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldReturnTrue_WhenProductExists()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Existing", Price = 10 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _service.DeleteProductAsync(1);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product?)null);

            // Act
            var result = await _service.DeleteProductAsync(1);

            // Assert
            Assert.False(result);
            _mockRepo.Verify(repo => repo.DeleteAsync(1), Times.Never);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldThrowException_WhenNameIsDuplicate()
        {
            // Arrange
            var dto = new CreateProductDto("Duplicate Name", 10m);
            _mockRepo.Setup(repo => repo.NameExistsAsync("Duplicate Name", null)).ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<DuplicatedProductNameException>(() => _service.CreateProductAsync(dto));
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldThrowException_WhenNameIsDuplicate()
        {
            // Arrange
            var dto = new UpdateProductDto("Duplicate Name", 10m);
            var existingProduct = new Product { Id = 1, Name = "Original Name", Price = 5m };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingProduct);
            // Check for "Duplicate Name", excluding ID 1
            _mockRepo.Setup(repo => repo.NameExistsAsync("Duplicate Name", 1)).ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<DuplicatedProductNameException>(() => _service.UpdateProductAsync(1, dto));
        }
    }
}