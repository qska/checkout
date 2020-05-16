using System;
using System.Threading.Tasks;
using AutoMapper;
using PaymentGateway.Banking;
using PaymentGateway.Banking.Contracts;
using PaymentGateway.Services.Models;

namespace PaymentGateway.Services
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly IBankingGateway bankingGateway;
        private readonly IPaymentService paymentService;
        private readonly IMapper mapper;

        public PaymentProcessor(IBankingGateway bankingGateway, IPaymentService paymentService, IMapper mapper)
        {
            this.bankingGateway = bankingGateway;
            this.paymentService = paymentService;
            this.mapper = mapper;
        }

        public async Task<PaymentResult> ProcessPaymentAsync(PaymentToProcess paymentRequest)
        {
            var bankingRequest = this.mapper.Map<BankingProcessPaymentRequest>(paymentRequest);
            var paymentData = this.mapper.Map<Payment>(paymentRequest);
            paymentData.Success = false;

            // masking the card details
            paymentData.CardNumber =
                paymentData.CardNumber.Substring(paymentData.CardNumber.Length - 4).PadLeft(16, '*');
            try
            {
                var bankingResponse = await this.bankingGateway.ProcessPaymentAsync(bankingRequest);
                paymentData.TransactionId = bankingResponse.PaymentTranscationId;

                if (bankingResponse.Status == StatusEnum.Success)
                {
                    paymentData.Success = true;
                }
                else
                {
                    paymentData.Success = false;
                }
            }
            catch (Exception)
            {
                paymentData.Success = false;

                // We still need the unique identifier. Disputable if we take the risk of guid value conflicts with the Banking Service.
                // Could also introduce an internal payment identifier, separate to the banking one.
                paymentData.TransactionId = Guid.NewGuid();
                // Log the error
            }

            // send pub/sub events here (if we want to notify any other systems)
            this.paymentService.AddPayment(paymentData);

            return new PaymentResult() { Success = paymentData.Success, TransactionId = paymentData.TransactionId };
        }
    }
}
