using System;
using PaymentGateway.Services.Models;

namespace PaymentGateway.Services
{
    public interface IPaymentService
    {
        Payment GetPaymentById(Guid id);
        void AddPayment(Payment paymentData);
    }
}
