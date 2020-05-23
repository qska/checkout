using System;

namespace PaymentGateway.Services.Models
{
    public class PaymentResult
    {
        public PaymentResult(Guid transcationId, bool success)
        {
            this.TransactionId = transcationId;
            this.Success = success;
        }

        public Guid TransactionId { get; }

        public bool Success { get; }
    }
}
