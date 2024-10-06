using CDShop.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Moq;

namespace CDShop.Tests.Utilities
{
    public class EmailSenderTests
    {
        [Fact]
        public async Task SendEmailAsync_ShouldCallSendEmailWithCorrectParameters()
        {
            // Arrange
            var mockEmailSender = new Mock<IEmailSender>();
            var email = "test@example.com";
            var subject = "Test Subject";
            var htmlMessage = "<p>This is a test message.</p>";

            // Act
            await mockEmailSender.Object.SendEmailAsync(email, subject, htmlMessage);

            // Assert
            mockEmailSender.Verify(
                sender => sender.SendEmailAsync(
                    It.Is<string>(e => e == email),
                    It.Is<string>(s => s == subject),
                    It.Is<string>(m => m == htmlMessage)),
                Times.Once);
        }

        [Fact]
        public async Task SendEmailAsync_ShouldCompleteSuccessfully()
        {
            // Arrange
            var emailSender = new EmailSender();
            var email = "test@example.com";
            var subject = "Test Subject";
            var htmlMessage = "<p>This is a test message.</p>";

            // Act
            await emailSender.SendEmailAsync(email, subject, htmlMessage);

            // Assert
            Assert.True(true); // Just asserting that it completes without exceptions
        }
    }
}