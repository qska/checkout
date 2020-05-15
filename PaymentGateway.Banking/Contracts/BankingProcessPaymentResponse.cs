using System;

namespace PaymentGateway.Banking.Contracts
{
    public class BankingProcessPaymentResponse
    {
        public StatusEnum Status { get; set; }
        public Guid PaymentTranscationId { get; set; }
    }
}
