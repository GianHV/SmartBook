using Common;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Net.payOS;
using Net.payOS.Types;
using PayingService.Models;

namespace PayingService.Services
{
    public class MyPayOS : IPayment
    {
        private readonly PayOS _payOS;
        private readonly string _connectionString;

        public MyPayOS(IConfiguration configuration)
        {
            _payOS = new PayOS(
                configuration.GetSection("Environment:PAYOS_CLIENT_ID").Value,
                configuration.GetSection("Environment:PAYOS_API_KEY").Value,
                configuration.GetSection("Environment:PAYOS_CHECKSUM_KEY").Value);
                _connectionString = configuration.GetSection("ConnectionStrings:DbConnectionString").Get<string>() ?? string.Empty;
        }

        public async Task<bool> CallBack(int orderCode)
        {
            PaymentLinkInformation paymentLinkInformation = await _payOS.getPaymentLinkInformation(orderCode);
            if(paymentLinkInformation.status == "PAID")
            {
                UpdatePaidTransaction(orderCode, paymentLinkInformation.amountPaid);
                return true;
            }
            return false;
        }

        public async Task<PaymentResponse> ProcessTransaction(PaymentRequest request)
        {
            var response = PaymentResponse.Pending;
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            var itemContent = GenerateContent(request.paymentType, request.amount, request.description);
            ItemData item = new ItemData(itemContent, 1, request.amount);
            List<ItemData> items = new List<ItemData> { item };

            PaymentData paymentData = new PaymentData(
                orderCode,
                request.amount,
                request.paymentType.ToString(),
                items,
                "https://www.awwwards.com/awwwards/collections/404-error-page",
                $"https://localhost:7206/api/paying/{orderCode}"
            );

            CreatePendingTransaction(request, orderCode);

            CreatePaymentResult createPayment =  await _payOS.createPaymentLink(paymentData);
            response.redirectUrl = createPayment.checkoutUrl;
            return response;
        }
        private void UpdatePaidTransaction(int orderCode, int amountPaid)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@paymentId", orderCode);
                parameters.Add("@amount", amountPaid);

                var result = conn.Execute("sp_add_transaction", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }
        private void CreatePendingTransaction(PaymentRequest request,int orderCode)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@paymentId", orderCode);
                parameters.Add("@userId", request.userId);
                parameters.Add("@amount", 0);
                parameters.Add("@type", request.paymentType.ToString());
                parameters.Add("@description", request.description);

                var result = conn.Execute("sp_add_transaction", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }

        private string GenerateContent(PaymentType paymentType, int amount, string description)
        {
            string content = string.Empty;
            switch (paymentType) {
                case PaymentType.DEPOSIT:
                    content = $"deposit money is {amount} dong";
                    break;
                case PaymentType.BORROW:
                    content = $"total amount money of borrow book is {amount} dong";
                    break;
                default:
                    content = $"money of compensation for reason: {description} is {amount} dong";
                    break;
            }
            return content;
        }
    }
}