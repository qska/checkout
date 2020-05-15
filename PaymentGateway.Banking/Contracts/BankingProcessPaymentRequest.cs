using System;

namespace PaymentGateway.Banking.Contracts
{
    public class BankingProcessPaymentRequest
    {
        public Guid MerchantId { get; set; }
        public string CardNumber { get; set; }
        public int StatusCode { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Cvv { get; set; }
    }
}
