using Common;
using Common.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using PayingService.Models;
using PayingService.Services;

namespace PayingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayingController : ApiControllerBase
    {
        private IConfiguration _configuration;
        public PayingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [Authorize]
        [HttpPost(Name = "create-payment-link")]
        public async Task<IActionResult> Checkout([FromBody] PaymentRequest request)
        {
            var role = HttpContext.GetRole();
            var payment = PaymentFactory.CreateInstance(role, _configuration);
            var redirect = await payment.ProcessTransaction(request);

            string url = processRedirect(redirect);

            return APIResponse(url);
        }

        [HttpGet("{orderCode}")]
        public async Task<IActionResult> UpdatePayment(int orderCode)
        {
            var payment = PaymentFactory.CreateInstance(Constants.CUSTOMER, _configuration);
            var result = await payment.CallBack(orderCode);
            return APIResponse(result ?
                "your money is sucessfully updated" :
                "fail transaction! ypur money is keeping, don't worry");
        }

        private string processRedirect(PaymentResponse redirect)
        {
            if(redirect.status == "PAID")
            {
                return string.Empty;
            }
            return redirect.redirectUrl;
        }
    }
}
