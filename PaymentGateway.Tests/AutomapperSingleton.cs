using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PaymentGateway.Banking.Contracts;
using PaymentGateway.Data;
using PaymentGateway.Responses;
using PaymentGateway.Services.Models;

namespace PaymentGateway.Tests
{
    public class AutomapperSingleton
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    // Auto Mapper Configurations
                    var mappingConfig = new MapperConfiguration(mc =>
                    {
                        mc.CreateMap<Payment, GetPaymentResponse>();
                        mc.CreateMap<PaymentToProcess, BankingProcessPaymentRequest>();
                        mc.CreateMap<PaymentToProcess, Payment>();
                        mc.CreateMap<Payment, PaymentData>();
                        mc.CreateMap<PaymentData, Payment>();
                    });
                    IMapper mapper = mappingConfig.CreateMapper();
                    _mapper = mapper;
                }

                return _mapper;
            }
        }
    }
}
