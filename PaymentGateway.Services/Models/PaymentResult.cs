using System;

namespace PaymentGateway.Services.Models
{
    public class PaymentResult
    {
        public Guid TransactionId { get; set; }

        public bool Success { get; set; }
    }
}
