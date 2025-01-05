using Dapper;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatalogService.Models;
using Common.Base;
using Microsoft.AspNetCore.Authorization;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ApiControllerBase
    {
        private readonly string _connectionString;
        public BooksController(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:DbConnectionString").Get<string>() ?? string.Empty;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] BookFindCreterias creterias)
        {
            var data = GetBooks(creterias);
            return APIResponse(data);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = GetBook(id);
            return APIResponse(data);
        }
        [HttpPost]
        public IActionResult Add([FromBody] BookRequestDto request)
        {
            var data = AddBook(request);
            return APIResponse(data);
        }
        [HttpPut]
        public IActionResult Put([FromBody] Book request)
        {
            EditBook(request);
            return APIResponse(string.Empty);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteBook(id);
            return APIResponse(string.Empty);
        }

        private void DeleteBook(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var result = conn.Execute("sp_delete_book", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }

        private void EditBook(Book book)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("id", book.Id);
                parameters.Add("@title", book.Title);
                parameters.Add("@description", book.Description);
                parameters.Add("@author", book.Author);
                parameters.Add("@genereId", book.GenereId);
                parameters.Add("@publisher", book.Publisher);
                parameters.Add("@language", book.Language);
                parameters.Add("@pageNumber", book.PageNumber);
                parameters.Add("@yearPublished", book.YearPublished);
                parameters.Add("@copiesTotal", book.CopiesTotal);
                parameters.Add("@copiesAvailable", book.CopiesAvailable);
                var result = conn.Execute("sp_edit_book", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }

        private BookResponseDto? GetBook(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var result = conn.Query<BookResponseDto>("sp_get_book", parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        private IEnumerable<BookResponseDto> GetBooks(BookFindCreterias creterias)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@title", creterias.Title);
                parameters.Add("@author", creterias.Author);
                parameters.Add("@genreId", creterias.GenreId);
                parameters.Add("@skip", creterias.Skip);
                parameters.Add("@take", creterias.Take);

                var result = conn.Query<BookResponseDto>("sp_get_books", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        private int AddBook(BookRequestDto book)
        {
            int newId = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@title", book.Title);
                parameters.Add("@description", book.Description);
                parameters.Add("@author", book.Author);
                parameters.Add("@genereId", book.GenereId);
                parameters.Add("@publisher", book.Publisher);
                parameters.Add("@language", book.Language);
                parameters.Add("@pageNumber", book.PageNumber);
                parameters.Add("@yearPublished", book.YearPublished);
                parameters.Add("@copiesTotal", book.CopiesTotal);
                parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                var result = conn.Execute("sp_add_books", parameters, null, commandType: CommandType.StoredProcedure);
                newId = parameters.Get<int>("@id");
            }
            return newId;
        }
    }
}
