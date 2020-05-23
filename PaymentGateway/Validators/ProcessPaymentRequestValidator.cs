using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using PaymentGateway.Requests;

namespace PaymentGateway.Validators
{
    public class ProcessPaymentRequestValidator : AbstractValidator<ProcessPaymentRequest>
    {
        public ProcessPaymentRequestValidator()
        {
            RuleFor(x => x.MerchantId).NotEmpty(); 
            RuleFor(x => x.Amount).NotEmpty().InclusiveBetween(0, Decimal.MaxValue);
            RuleFor(x => x.CardNumber).Length(16);
            RuleFor(x => x.Currency).Length(3);
            RuleFor(x => x.Cvv).Length(3);
            RuleFor(x => x.ExpiryMonth).InclusiveBetween(1, 12);
            RuleFor(x => x.ExpiryYear).GreaterThan(DateTime.Today.Year - 1);
            RuleFor(x => x.CardNumber)
                .Must(number =>
                {
                    if (number == null)
                    {
                        return false;
                    }
                    return number.All(char.IsDigit) && number.Reverse()
                               .Select(c => c - 48)
                               .Select((thisNum, i) => i % 2 == 0
                                   ? thisNum
                                   : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                               ).Sum() % 10 == 0;
                })
                .WithMessage("Card number validation failed");
        }
    }
}
