using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Responses
{
    public class GetPaymentResponse
    {
        public Guid TransactionId { get; set; }
        public string CardNumber { get; set; }
        public int StatusCode { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Cvv { get; set; }
        public bool Success { get; set; }
    }
}
