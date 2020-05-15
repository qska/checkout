using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Banking;
using PaymentGateway.Banking.Contracts;
using PaymentGateway.Data;
using PaymentGateway.Requests;
using PaymentGateway.Responses;
using PaymentGateway.Services;
using PaymentGateway.Services.Models;

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPaymentProcessor paymentProcessor;
        private readonly IPaymentService paymentService;
        
        public PaymentsController(IMapper mapper, IPaymentProcessor paymentProcessor, IPaymentService paymentService)
        {
            this.mapper = mapper;
            this.paymentProcessor = paymentProcessor;
            this.paymentService = paymentService;
        }

        [HttpGet("{id}")]
        public ActionResult<GetPaymentResponse> Get(Guid paymentId)
        {
            // ensure that the api caller can actually obtain these payment details.
            // introduce authentication, and to authorise - verify on MerchantId

            // Also add exception handling, log and return a nice 500 response.
            var paymentData = this.paymentService.GetPaymentById(paymentId);
            if (paymentData != null)
            {
                return mapper.Map<GetPaymentResponse>(paymentData);
            }

            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<ProcessPaymentResponse>> ProcessPaymentAsync([FromBody] ProcessPaymentRequest paymentRequest)
        {
            // ensure that the api caller can actually obtain these payment details.
            // introduce authentication, and to authorise - verify on MerchantId
            //
            // Also add exception handling, log and return a nice 500 response.
            var transactionResult = await this.paymentProcessor.ProcessPaymentAsync(mapper.Map<PaymentToProcess>(paymentRequest));
            return new ActionResult<ProcessPaymentResponse>(new ProcessPaymentResponse() { TransactionId = transactionResult.TransactionId, Success = transactionResult.Success });
        }
    }
}
