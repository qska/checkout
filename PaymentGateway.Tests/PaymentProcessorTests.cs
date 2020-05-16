using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using PaymentGateway.Banking;
using PaymentGateway.Banking.Contracts;
using PaymentGateway.Services;
using PaymentGateway.Services.Models;
using Xunit;

namespace PaymentGateway.Tests
{
    public class PaymentProcessorTests
    {
        [Fact]
        public async Task ProcessPaymentExecutesSuccessfully()
        {
            // Arrange
            var mockBankingGateway = new Mock<IBankingGateway>();
            mockBankingGateway.Setup(x => x.ProcessPaymentAsync(It.IsAny<BankingProcessPaymentRequest>()))
                .ReturnsAsync(new BankingProcessPaymentResponse()
                    { PaymentTranscationId = Guid.NewGuid(), Status = StatusEnum.Success});
            var mockPaymentService = new Mock<IPaymentService>();

            var sut = new PaymentProcessor(mockBankingGateway.Object, mockPaymentService.Object,
                AutoMapperSingleton.Mapper);

            // Act
            var result = await sut.ProcessPaymentAsync(new PaymentToProcess()
            {
                Amount = 10, CardNumber = "1234567890123456", Currency = "GBP", Cvv = "000", ExpiryMonth = 10,
                ExpiryYear = 2020, MerchantId = Guid.NewGuid()
            });

            // Assert
            result.Success.Should().BeTrue();
            result.TransactionId.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ProcessPaymentFailsGracefully()
        {
            // Arrange
            var mockBankingGateway = new Mock<IBankingGateway>();
            var mockPaymentService = new Mock<IPaymentService>();

            var sut = new PaymentProcessor(mockBankingGateway.Object, mockPaymentService.Object,
                AutoMapperSingleton.Mapper);

            // Act
            // this will result in a null ref exception in the banking gateway
            var result = await sut.ProcessPaymentAsync(new PaymentToProcess()
            {
                Amount = 10,
                CardNumber = "1234567890123456",
                Currency = "GBP",
                Cvv = "000",
                ExpiryMonth = 10,
                ExpiryYear = 2020,
                MerchantId = Guid.NewGuid()
            });

            // Assert
            result.Success.Should().BeFalse();
            result.TransactionId.Should().NotBeEmpty();
        }
    }
}
