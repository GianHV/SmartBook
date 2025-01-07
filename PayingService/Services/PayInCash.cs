using Dapper;
using System.Data.SqlClient;
using System.Data;
using PayingService.Models;
using Common;
using Microsoft.OpenApi.Extensions;

namespace PayingService.Services
{
    internal class PayInCash : IPayment
    {
        private readonly string _connectionString;
        public PayInCash(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:DbConnectionString").Get<string>() ?? string.Empty;
        }

        public Task<bool> CallBack(int orderCode)
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentResponse> ProcessTransaction(PaymentRequest request)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@paymentId", $"paycash-{StringExt.GenerateRandomGUID(12)}");
                parameters.Add("@userId", request.userId);
                parameters.Add("@amount", request.amount);
                parameters.Add("@type", request.paymentType.ToString());
                parameters.Add("@description", request.description);

                try
                {
                    var result = conn.Execute("sp_add_transaction", parameters, null, commandType: CommandType.StoredProcedure);

                }
                catch(SqlException)
                {
                    throw;
                }
            }
            return PaymentResponse.Success;
        }
    }
}