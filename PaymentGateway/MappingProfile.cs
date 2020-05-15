using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PaymentGateway.Banking.Contracts;
using PaymentGateway.Data;
using PaymentGateway.Requests;
using PaymentGateway.Responses;
using PaymentGateway.Services.Models;

namespace PaymentGateway
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // this should be many classes, and covered by unit tests
            CreateMap<Payment, GetPaymentResponse>();
            CreateMap<PaymentToProcess, BankingProcessPaymentRequest>();
            CreateMap<PaymentToProcess, Payment>();
            CreateMap<Payment, PaymentData>();
            CreateMap<PaymentData, Payment>();
            CreateMap<ProcessPaymentRequest, PaymentToProcess>();
        }
    }
}
