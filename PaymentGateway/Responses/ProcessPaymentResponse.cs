using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Responses
{
    public class ProcessPaymentResponse
    {
        public Guid TransactionId { get; set; }

        public bool Success { get; set; }
    }
}
