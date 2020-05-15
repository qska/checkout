using System.Threading.Tasks;
using PaymentGateway.Services.Models;

namespace PaymentGateway.Services
{
    public interface IPaymentProcessor
    {
        Task<PaymentResult> ProcessPaymentAsync(PaymentToProcess map);
    }
}