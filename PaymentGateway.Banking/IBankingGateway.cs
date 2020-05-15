using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PaymentGateway.Banking.Contracts;

namespace PaymentGateway.Banking
{
    public interface IBankingGateway
    {
        Task<BankingProcessPaymentResponse> ProcessPaymentAsync(BankingProcessPaymentRequest request);
    }
}
