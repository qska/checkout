using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Retrieves and returns the Payment details for a given payment Id.
        /// </summary>
        /// <param name="paymentId">PaymentId, previously obtained from ProcessPaymentAsync endpoint</param>
        /// <returns>Payment details</returns>
        [HttpGet("{paymentId}")]
        [Authorize]
        public ActionResult<GetPaymentResponse> Get(Guid paymentId)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if (currentUser.HasClaim(c => c.Type == "abc"))
            {
            }

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

        /// <summary>
        /// Validates and executes the payment request.
        /// </summary>
        /// <param name="paymentRequest">Payment request object</param>
        /// <returns>Transaction Id</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProcessPaymentResponse>> ProcessPaymentAsync([FromBody] ProcessPaymentRequest paymentRequest)
        {
            // ensure that the api caller can actually obtain these payment details.
            // introduce authentication, and to authorise - verify on MerchantId
            // Plumb in request model validation
            //
            // Also add exception handling, log and return a nice 500 response.
            var transactionResult = await this.paymentProcessor.ProcessPaymentAsync(mapper.Map<PaymentToProcess>(paymentRequest));

            // We might want to return a 201 response and the GET Url for the payment we just created.
            return new ActionResult<ProcessPaymentResponse>(new ProcessPaymentResponse() { TransactionId = transactionResult.TransactionId, Success = transactionResult.Success });
        }
    }
}
