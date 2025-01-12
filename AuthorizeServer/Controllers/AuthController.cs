using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using AuthorizeServer.Helpers;
using AuthorizeServer.Models;
using Common.Base;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace AuthorizeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly string _connectionString;
        public AuthController(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:DbConnectionString").Get<string>() ?? string.Empty;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserRequestDto request)
        {
            var userDto = GetInfor(request);
            if (userDto == null) return APIResponse(userDto, HttpStatusCode.Unauthorized);

            var response = new AuthResponse();
            response.AccessToken = GenerateAccessToken(userDto);

            return APIResponse(response);
        }

        [HttpPost("add")]
        [Authorize(Roles = "employee,admin")]

        [HttpGet("customer")]
        [Authorize(Roles = "employee,admin")]
        public IActionResult GetCustomer([FromQuery] string username)
        {
            var response = GetCustomers(username);
            return APIResponse(response);
        }

        private IEnumerable<UserDTO> GetCustomers(string username)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@username", username);
                var result = conn.Query<UserDTO>("sp_get_customers", parameters, null, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public IActionResult AddNewUser([FromBody] User request)
        {
            string isSuccess = AddingUser(request);
            if (isSuccess.IsNullOrEmpty()) {
                return APIResponse(string.Empty);
            }
            var response = new ApiResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccess = false,
                ErrorMessages = new List<string> { isSuccess }
            };
            return BadRequest(response);
        }

        private string AddingUser(User user)
        {
            string error = string.Empty;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@cityzenNumber", user.cityzenNumber);
                parameters.Add("@fullName", user.fullName);
                parameters.Add("@username", user.username);
                parameters.Add("@password", user.password);
                parameters.Add("@phoneNumber", user.phoneNumber);
                parameters.Add("@role", user.role);
                try
                {
                    var result = conn.Execute("sp_add_user", parameters, null, commandType: CommandType.StoredProcedure);
                }

                catch (Exception ex) {
                    error = ex.Message;
                }
            }
            return error;
        }
        private string GenerateAccessToken(UserDTO userDto)
        {
            var claims = new List<Claim>
            {
                new Claim(type: "id", value: userDto.cityzenNumber),
                new Claim(ClaimTypes.Name,userDto.fullName),
                new Claim(ClaimTypes.Role,userDto.role),
            };

            RsaSecurityKey rsaSecurityKey = GeneratorKey.GetRsaKey();
            var signingCredentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

            var token = new JwtSecurityToken(
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(20),
                  signingCredentials: signingCredentials
                  );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserDTO? GetInfor(UserRequestDto request)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@username", request.username);
                parameters.Add("@password", request.password);
                var result = conn.Query<UserDTO>("sp_login", parameters, null, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }
       
    }
}
