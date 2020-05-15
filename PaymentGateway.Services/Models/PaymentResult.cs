using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Services.Models
{
    public class PaymentResult
    {
        public Guid TransactionId { get; set; }

        public bool Success { get; set; }
    }
}
