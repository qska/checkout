﻿using System;

namespace PaymentGateway.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public Guid MerchantId { get; set; }
    }
}
