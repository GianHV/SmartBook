using Common;
using Common.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using PayingService.Models;
using PayingService.Services;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace PayingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayingController : ApiControllerBase
    {
        private IConfiguration _configuration;
        private readonly string _connectionString;
        public PayingController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetSection("ConnectionStrings:DbConnectionString").Get<string>() ?? string.Empty;


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

            // redirect homepage if it result is true
            return APIResponse(result ?
                "your money is sucessfully updated" :
                "fail transaction! ypur money is keeping, don't worry");
        }

        [Authorize]
        [HttpGet("getBalance")]
        public IActionResult GetBalance()
        {
            int money = 0;
            if(HttpContext.GetRole() == "customer")
            {
                money = GetMoney(HttpContext.GetUserId());
            }
            return APIResponse(money);
        }

        private int GetMoney(string userId)
        {
            int dayLater = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@userId", userId);

                var result = conn.Query<int>("sp_get_coin", parameters, null, commandType: CommandType.StoredProcedure);
                if (result.Any()) dayLater = result.FirstOrDefault();
            }
            return dayLater;
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
