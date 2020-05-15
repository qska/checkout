using PaymentGateway.Services.Models;

namespace PaymentGateway.Services
{
    public interface IPaymentProcessor
    {
        PaymentResult ProcessPayment(PaymentToProcess map);
    }
}