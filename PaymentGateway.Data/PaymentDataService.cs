using System;

namespace PaymentGateway.Data
{
    public class PaymentDataService : IPaymentDataService
    {
        public PaymentData GetPaymentData(Guid id)
        {
            // Add persistence - SQL Server would work
            return new PaymentData() { Success = true, Amount = 10, TransactionId = id, Currency = "GBP", Cvv = "000", CardNumber = "************1234", ExpiryMonth = 10, ExpiryYear = 2020 };
        }

        public void InsertPaymentData(PaymentData paymentData)
        {
            // Add persistence - a simple SQL database will work to start with.
        }
    }
}
