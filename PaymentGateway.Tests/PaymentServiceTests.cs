using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using PaymentGateway.Data;
using PaymentGateway.Services;
using PaymentGateway.Services.Models;
using Xunit;

namespace PaymentGateway.Tests
{
    public class PaymentServiceTests
    {
        [Theory]
        [AutoData]
        public void AddPaymentSucceeds(Payment payment)
        {
            // Arrange
            var mockPaymentDataService = new Mock<IPaymentDataService>();
            PaymentData dataToWrite = null;
            mockPaymentDataService.Setup(x => x.InsertPaymentData(It.IsAny<PaymentData>()))
                .Callback((PaymentData pd) => dataToWrite = pd);
            var sut = new PaymentService(mockPaymentDataService.Object, AutomapperSingleton.Mapper);

            // Act
            sut.AddPayment(payment);

            // Assert
            mockPaymentDataService.Verify(x => x.InsertPaymentData(It.IsAny<PaymentData>()));
            dataToWrite.Should().BeEquivalentTo(payment);
        }

        [Fact]
        public void ReadingPaymentSucceeds()
        {

            // Arrange
            var mockPaymentDataService = new Mock<IPaymentDataService>();
            PaymentData dataToWrite = null;
            var paymentData = new PaymentData() { Success = true, TransactionId = Guid.NewGuid(), Amount = 10, CardNumber = "1234567890123456", Currency = "GBP", Cvv = "999", ExpiryMonth = 10, ExpiryYear = 2020 };
            mockPaymentDataService.Setup(x => x.GetPaymentData(It.IsAny<Guid>()))
                .Returns(paymentData);
            var sut = new PaymentService(mockPaymentDataService.Object, AutomapperSingleton.Mapper);

            // Act
            var result = sut.GetPaymentById(Guid.NewGuid());

            // Assert
            mockPaymentDataService.Verify(x => x.GetPaymentData(It.IsAny<Guid>()));
            result.Should().BeEquivalentTo(paymentData);
        }
    }
}
