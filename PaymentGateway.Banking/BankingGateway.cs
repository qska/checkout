using System;
using PaymentGateway.Banking.Contracts;

namespace PaymentGateway.Banking
{
    public class BankingGateway : IBankingGateway
    {
        public BankingProcessPaymentResponse ProcessPayment(BankingProcessPaymentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
