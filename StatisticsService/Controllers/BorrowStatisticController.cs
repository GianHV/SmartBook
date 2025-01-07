using System.Data.SqlClient;
using System.Data;
using Common.Base;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StatisticsService.Alternative;
using StatisticsService.Models;

namespace StatisticsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,employee")]
    public class BorrowStatisticController : ApiControllerBase
    {
        private readonly string _connectionString;
        private readonly IBorrowService _borrowService;
        public BorrowStatisticController(
            IBorrowService borrowService,
            IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:DbConnectionString").Get<string>() ?? string.Empty;
            _borrowService = borrowService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecordToday()
        {
            var records = await GetRecords();
            var cnt = records.Where(u => u.borrowDate == DateTime.Today).Count();
            ProcessReports(cnt);
            return APIResponse(cnt);
        }

        [HttpGet("range")]
        public IActionResult GetRecordToday([FromQuery] BorrowCreterias creterias)
        {
            var data = GetReports(creterias);
            return APIResponse(data);
        }

        private IEnumerable<Report> GetReports(BorrowCreterias creterias)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@from", creterias.from);
                parameters.Add("@to", creterias.to);

                var result = conn.Query<Report>("sp_get_reports", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        private void ProcessReports(int cnt)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@totalAmount", cnt);

                var result = conn.Execute("sp_add_report", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }

        private async Task<List<Borrow>> GetRecords()
        {
            List<Borrow> records = new List<Borrow>();
            var token = HttpContext.Items["BearerToken"]?.ToString() ?? string.Empty;
            var response = await _borrowService.GetBorrow<ApiResponse>(token);
            if (response != null && response.IsSuccess)
            {
                records = JsonConvert.DeserializeObject<List<Borrow>>(Convert.ToString(response.Result));
            }
            return records;
        }
    }
}
