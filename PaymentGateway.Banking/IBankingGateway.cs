using System;
using System.Collections.Generic;
using System.Text;
using PaymentGateway.Banking.Contracts;

namespace PaymentGateway.Banking
{
    public interface IBankingGateway
    {
        BankingProcessPaymentResponse ProcessPayment(BankingProcessPaymentRequest request);
    }
}
