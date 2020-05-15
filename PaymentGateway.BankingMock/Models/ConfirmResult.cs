using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.BankingMock.Models
{
    public class ConfirmResult
    {
        public Guid TransactionId => Guid.NewGuid();
    }
}
