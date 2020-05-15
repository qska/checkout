using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Banking.Contracts
{
    public class ConfirmResult
    {
        public Guid TransactionId { get; set; }
    }
}
