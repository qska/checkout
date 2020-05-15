﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Services.Models
{
    public class PaymentToProcess
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
