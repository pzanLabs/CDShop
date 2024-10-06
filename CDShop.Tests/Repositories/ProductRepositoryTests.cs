using CDShop.DataAccess;
using CDShop.DataAccess.Data;
using CDShop.DataAccess.Repository;
using CDShop.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CDShop.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> _options;

        public ProductRepositoryTests()
        {
            // Set up the in-memory database options
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CDShopTestDB")
                .Options;
        }

        [Fact]
        public void GetProductById_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            using (var context = new ApplicationDbContext(_options))
            {
                // Add a product to the in-memory database with all required properties set
                context.Products.Add(new Product
                {
                    Id = productId,
                    Title = "Test Title",
                    Author = "Test Author",
                    Description = "Test Description",
                    Year = "2024",
                    ListPrice = 100,
                    Price = 90,
                    Price50 = 80,
                    Price100 = 70,
                    GenreId = 1,
                    PackageId = 1
                });
                context.SaveChanges();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new ProductRepository(context);
                var product = repository.GetProductById(productId);

                // Assert
                Assert.NotNull(product);
                Assert.Equal("Test Title", product.Title);
                Assert.Equal("Test Author", product.Author);
                Assert.Equal("Test Description", product.Description);
                Assert.Equal("2024", product.Year);
                Assert.Equal(100, product.ListPrice);
                Assert.Equal(90, product.Price);
                Assert.Equal(80, product.Price50);
                Assert.Equal(70, product.Price100);
                Assert.Equal(1, product.GenreId);
                Assert.Equal(1, product.PackageId);
            }
        }

        [Fact]
        public void GetProductById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = 2;

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new ProductRepository(context);
                var product = repository.GetProductById(productId);

                // Assert
                Assert.Null(product);
            }
        }
    }
}