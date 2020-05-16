using System;

namespace PaymentGateway.Data
{
    public interface IPaymentDataService
    {
        PaymentData GetPaymentData(Guid id);
        void InsertPaymentData(PaymentData paymentData);
    }
}
