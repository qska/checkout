using System;

namespace PaymentGateway.Data
{
    public class PaymentDataService : IPaymentDataService
    {
        public PaymentData GetPaymentData(Guid id)
        {
            // Add persistence - SQL Server would work
            throw new NotImplementedException();
        }

        public void InsertPaymentData(PaymentData paymentData)
        {
            // Add persistence - SQL Server would work
            throw new NotImplementedException();
        }
    }
}
