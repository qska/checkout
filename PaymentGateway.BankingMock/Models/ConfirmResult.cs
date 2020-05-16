using System;

namespace PaymentGateway.BankingMock.Models
{
    public class ConfirmResult
    {
        public Guid TransactionId => Guid.NewGuid();
    }
}
