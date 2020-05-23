using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using FluentAssertions;
using FluentValidation.TestHelper;
using PaymentGateway.Requests;
using PaymentGateway.Validators;
using Xunit;

namespace PaymentGateway.Tests
{
    public class ProcessPaymentRequestValidatorTests
    {
        private ProcessPaymentRequestValidator validator;

        public ProcessPaymentRequestValidatorTests()
        {
            this.validator = new ProcessPaymentRequestValidator();
        }
         
        [Fact]
        public void MerchantIdIsRequired()
        {
            this.validator.ShouldHaveValidationErrorFor(m => m.MerchantId, Guid.Empty);
        }

        [Fact]
        public void AmountCannotBeNegative()
        {
            this.validator.ShouldHaveValidationErrorFor(x => x.Amount, -10);
        }

        [Theory]
        [InlineData("G")]
        [InlineData("GB")]
        [InlineData("GBPP")]
        public void CurrencyLengthHasToBe3(string currency)
        {
            this.validator.ShouldHaveValidationErrorFor(x => x.Currency, currency);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("12")]
        [InlineData("1231")]
        public void CvvLengthHasToBe3(string cvv)
        {
            this.validator.ShouldHaveValidationErrorFor(x => x.Currency, cvv);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(13)]
        public void ExpiryMonthHasToBeValid(int expiryMonth)
        {
            this.validator.ShouldHaveValidationErrorFor(x => x.ExpiryMonth, expiryMonth);
        }

        [Theory]
        [InlineData(2019)]
        [InlineData(1980)]
        public void ExpiryYearHasToBeValid(int expiryYear)
        {
            this.validator.ShouldHaveValidationErrorFor(x => x.ExpiryYear, expiryYear);
        }

        [Fact]
        public void PassesForAValidObject()
        {
            // Arrange
            // Act
            var result = this.validator.Validate(new ProcessPaymentRequest()
            {
                CardNumber = "2720992378118650",
                MerchantId = new Guid("be9e09d5-2bf9-45b5-b35b-de3d68f249d4"),
                ExpiryMonth = 1,
                ExpiryYear = 2020,
                Amount = 10,
                Currency = "GBP",
                Cvv = "000"
            });

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
