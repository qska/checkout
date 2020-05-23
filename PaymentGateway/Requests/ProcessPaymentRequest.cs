using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Requests
{
    public class ProcessPaymentRequest
    {
        public Guid MerchantId { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Cvv { get; set; }
    }
}
