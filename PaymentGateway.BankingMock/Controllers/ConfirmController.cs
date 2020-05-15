using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.BankingMock.Models;

namespace PaymentGateway.BankingMock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmController : ControllerBase
    {
        [HttpPost]
        public ActionResult<ConfirmResult> Post()
        {
            return new ConfirmResult();
        }
    }
}
