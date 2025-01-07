using System.Data.SqlClient;
using System.Data;
using Common.Base;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecordService.Models;
using Microsoft.AspNetCore.Authorization;
using RecordService.Alternative;
using Common;

namespace RecordService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ApiControllerBase
    {
        private readonly string _connectionString;
        private readonly IBookService _bookService;
        private readonly IPayingService _payingService;
        public BorrowController(
            IConfiguration configuration,
            IBookService bookService,
            IPayingService payingService)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:DbConnectionString").Get<string>() ?? string.Empty;
            _bookService = bookService;
            _payingService = payingService;
        }

        [HttpPost(Name = "borrow")]
        [Authorize(Roles = "employee,admin")]
        public async Task<IActionResult> Add([FromBody] BorrowRequestDto request)
        {
            if(!await UpdateQuantityBook(new BookDto() { id = request.bookId, copiesAvailable = -1 }))
            {
                var response = new ApiResponse()
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string>() { "The number of book is not enough" }
                };
                return StatusCode((int)response.StatusCode, response);
            }
            if(!await UpdateMoney(new PaymentDto() {  userId = request.userId, amount = -2000, description = "tien thue sach"})){
                var response = new ApiResponse()
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string>() { "The money of this user is not enough to hire a book" }
                };
                return StatusCode((int)response.StatusCode, response);
            }
            var data = AddRecord(request);
            return APIResponse(data);
        }

        [Authorize(Roles = "employee,admin")]
        [HttpPut(Name = "return")]

        public async Task<IActionResult> Return([FromBody] int id)
        {
            var infor = GetBorrowRecord(id);
            if(infor?.status == BorrowStatus.RETURN)
            {
                var response = new ApiResponse()
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string>() { "This book was payed. you can't do this action" }
                };
            }
            var dateLater = ReturnBook(id);
            await UpdateQuantityBook(new BookDto() { id = infor.bookId, copiesAvailable = 1 });
            if (dateLater > 0)
            {
                await UpdateMoney(new PaymentDto()
                {
                    userId = infor.userId,
                    amount = -1000 * dateLater,
                    description = "tien thue sach"
                });

            }
            return APIResponse(dateLater > 0 ? $"Lating {dateLater} days" : "");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Gets()
        {
            string userId = null;
            var role = HttpContext.GetRole();
            if(role == "customer") userId = HttpContext.GetUserId();
            var data = GetRecords(userId);
            return APIResponse(data);
        }

        private IEnumerable<Borrow> GetRecords(string userId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@userId", userId);

                var result = conn.Query<Borrow>("sp_get_records", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        private BorrowDto? GetBorrowRecord(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var result = conn.Query<BorrowDto>("sp_get_borrow_record", parameters, null, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        private async Task<bool> UpdateMoney(PaymentDto paymentDto)
        {
            var token = HttpContext.Items["BearerToken"]?.ToString() ?? string.Empty;
            var response = await _payingService.UpdateMoney<ApiResponse>(paymentDto, token);
            if (response != null && response.IsSuccess == false)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> UpdateQuantityBook(BookDto bookDto)
        {
            var token = HttpContext.Items["BearerToken"]?.ToString() ?? string.Empty;
            var response = await _bookService.UpdateProduct<ApiResponse>(bookDto, token);
            if (response != null && response.IsSuccess == false)
            {
                return false;
            }
            return true;
        }

        private int ReturnBook(int id)
        {
            int dayLater = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var result = conn.Query<int>("sp_return_book", parameters, null, commandType: CommandType.StoredProcedure);
                if(result.Any()) dayLater = result.FirstOrDefault();
            }
            return dayLater;
        }

        private int AddRecord(BorrowRequestDto request)
        {
            int newId = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@userId", request.userId);
                parameters.Add("@bookId", request.bookId);
                parameters.Add("@dueDate", request.dueDate);
                parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                var result = conn.Execute("sp_borrow_book", parameters, null, commandType: CommandType.StoredProcedure);
                newId = parameters.Get<int>("@id");
            }
            return newId;
        }
    }
}
