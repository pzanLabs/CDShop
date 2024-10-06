using CDShop.DataAccess.Repository.IRepository;
using CDShop.Models.ViewModels;
using CDShop.Models;
using CDShopWeb.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CDShop.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _controller = new ProductController(_unitOfWorkMock.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }

        [Fact]
        public void Upsert_Post_ValidModel_AddsNewProduct()
        {
            // Arrange
            var productVM = new ProductVM
            {
                Product = new Product { Id = 0, Title = "New Product" }
            };

            _unitOfWorkMock.Setup(u => u.Product.Add(It.IsAny<Product>())).Verifiable();
            _unitOfWorkMock.Setup(u => u.Save()).Verifiable();

            // Act
            var result = _controller.Upsert(productVM);

            // Assert
            _unitOfWorkMock.Verify(u => u.Product.Add(It.IsAny<Product>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void Upsert_Post_ValidModel_UpdatesExistingProduct()
        {
            // Arrange
            var productVM = new ProductVM
            {
                Product = new Product { Id = 1, Title = "Existing Product" }
            };

            _unitOfWorkMock.Setup(u => u.Product.Update(It.IsAny<Product>())).Verifiable();
            _unitOfWorkMock.Setup(u => u.Save()).Verifiable();

            // Act
            var result = _controller.Upsert(productVM);

            // Assert
            _unitOfWorkMock.Verify(u => u.Product.Update(It.IsAny<Product>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }
    }
}
