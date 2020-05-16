using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using PaymentGateway.Controllers;
using PaymentGateway.Services;
using PaymentGateway.Services.Models;
using Xunit;

namespace PaymentGateway.Tests
{
    public class PaymentsControllerTests
    {
        [Theory]
        [AutoData]
        public void GetPaymentByIdReturnsData(Payment mockPayment)
        {
            // Arrange
            var mockPaymentService = new Mock<IPaymentService>();
            var paymentId = Guid.NewGuid();
            mockPayment.TransactionId = paymentId;

            mockPaymentService.Setup(x => x.GetPaymentById(paymentId)).Returns(mockPayment);
            var controller = new PaymentsController(AutoMapperSingleton.Mapper, new Mock<IPaymentProcessor>().Object, mockPaymentService.Object );

            // Act
            var result = controller.Get(paymentId);

            // Assert
            result.Value.Should().BeEquivalentTo(mockPayment);
        }

        [Fact]
        public void GetPaymentByIdReturns404()
        {
            // Arrange
            var mockPaymentService = new Mock<IPaymentService>();
            var paymentId = Guid.NewGuid();

            mockPaymentService.Setup(x => x.GetPaymentById(paymentId)).Returns((Payment)null);
            var controller = new PaymentsController(AutoMapperSingleton.Mapper, new Mock<IPaymentProcessor>().Object, mockPaymentService.Object);

            // Act
            var result = controller.Get(paymentId);

            // Assert
            result.Result.Should().BeOfType<Microsoft.AspNetCore.Mvc.NotFoundResult>();
        }

        // repeat similar tests for the POST endpoint
    }
}
