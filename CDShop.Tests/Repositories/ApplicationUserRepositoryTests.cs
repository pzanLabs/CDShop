using CDShop.DataAccess;
using CDShop.DataAccess.Data;
using CDShop.DataAccess.Repository;
using CDShop.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CDShop.Tests.Repositories
{
    public class ApplicationUserRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> _options;

        public ApplicationUserRepositoryTests()
        {
            // Set up the in-memory database options
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CDShopTestDB")
                .Options;
        }

        [Fact]
        public void GetApplicationUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userName = "Test User";
            using (var context = new ApplicationDbContext(_options))
            {
                var company = new Company
                {
                    Id = 1,
                    Name = "Test Company"
                };
                context.Companies.Add(company);

                // Add an ApplicationUser to the in-memory database with all required properties set
                context.ApplicationUsers.Add(new ApplicationUser
                {
                    Name = userName,
                    StreetAddress = "123 Test St",
                    City = "Testville",
                    State = "TS",
                    PostalCode = "12345",
                    CompanyId = 1
                });
                context.SaveChanges();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new ApplicationUserRepository(context);
                var user = repository.GetApplicationUserByName(userName);

                // Assert
                Assert.NotNull(user);
                Assert.Equal("Test User", user.Name);
                Assert.Equal("123 Test St", user.StreetAddress);
                Assert.Equal("Testville", user.City);
                Assert.Equal("TS", user.State);
                Assert.Equal("12345", user.PostalCode);
            }
        }

        [Fact]
        public void GetApplicationUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userName = "Other user";

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new ApplicationUserRepository(context);
                var user = repository.GetApplicationUserByName(userName);

                // Assert
                Assert.Null(user);
            }
        }
    }
}