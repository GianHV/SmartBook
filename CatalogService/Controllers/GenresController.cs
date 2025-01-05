using System.Data.SqlClient;
using System.Data;
using CatalogService.Models;
using Common.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenresController : ApiControllerBase
    {
        private readonly string _connectionString;
        public GenresController(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:DbConnectionString").Get<string>() ?? string.Empty;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = GetGenres();
            return APIResponse(data);
        }

        private IEnumerable<Genre> GetGenres()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var result = conn.Query<Genre>("sp_get_genres", null, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
