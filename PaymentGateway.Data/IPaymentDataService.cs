using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Data
{
    public interface IPaymentDataService
    {
        PaymentData GetPaymentData(Guid id);
        void InsertPaymentData(PaymentData paymentData);
    }
}
