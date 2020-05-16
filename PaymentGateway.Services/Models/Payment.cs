using System;

namespace PaymentGateway.Services.Models
{
    public class Payment
    {
        public Guid TransactionId { get; set; }
        public string CardNumber { get; set; }
        public bool Success { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Cvv { get; set; }
    }
}
