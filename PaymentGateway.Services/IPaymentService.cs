using System;
using System.Collections.Generic;
using System.Text;
using PaymentGateway.Services.Models;

namespace PaymentGateway.Services
{
    public interface IPaymentService
    {
        Payment GetPaymentById(Guid id);
        void AddPayment(Payment paymentData);
    }
}
