using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        // Note - I couldn't quite get the locally issued tokens to work correctly, hence the Bearer token is not currently required.
        // In proper environments we would have used an existing IdentityServer
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
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

        /// <summary>
        /// Validates and executes the payment request.
        /// </summary>
        /// <param name="paymentRequest">Payment request object</param>
        /// <returns>Transaction Id</returns>
        [HttpPost]
        // Note - I couldn't quite get the locally issued tokens to work correctly, hence the Bearer token is not currently required.
        // In proper environments we would have used an existing IdentityServer
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<ActionResult<ProcessPaymentResponse>> ProcessPaymentAsync([FromBody] ProcessPaymentRequest paymentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // ensure that the api caller can actually obtain these payment details. This would work on comparing MerchantId, when the authentication is plumbed in fully.
            var transactionResult = await this.paymentProcessor.ProcessPaymentAsync(mapper.Map<PaymentToProcess>(paymentRequest));

            return new ActionResult<ProcessPaymentResponse>(new ProcessPaymentResponse() { TransactionId = transactionResult.TransactionId, Success = transactionResult.Success });
        }
    }
}
