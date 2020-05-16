using System;
using AutoMapper;
using PaymentGateway.Data;
using PaymentGateway.Services.Models;

namespace PaymentGateway.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDataService paymentDataService;
        private readonly IMapper mapper;

        public PaymentService(IPaymentDataService paymentDataService, IMapper mapper)
        {
            this.paymentDataService = paymentDataService;
            this.mapper = mapper;
        }
        public Payment GetPaymentById(Guid id)
        {
            // nulls will bubble up to the calling code
            var paymentData = this.paymentDataService.GetPaymentData(id);
            var result = mapper.Map<Payment>(paymentData);
            return result;
        }

        public void AddPayment(Payment payment)
        {
            var paymentData = mapper.Map<PaymentData>(payment);
            this.paymentDataService.InsertPaymentData(paymentData);
        }
    }
}
