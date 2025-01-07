
using Common.Base;
using Newtonsoft.Json.Linq;
using RecordService.Models;

namespace RecordService.Alternative
{
    public class BookService : HttpService,IBookService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string bookUrl;
        public BookService(
            IHttpClientFactory clientFactory,
            IConfiguration configuration)
            : base(clientFactory)
        {
            _clientFactory = clientFactory;
            bookUrl = configuration.GetValue<string>("ServiceUrls:bookAPI");
        }
        public Task<T> UpdateProduct<T>(BookDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.PUT,
                Data = dto,
                Url = bookUrl + "/api/books",
                Token = token,
            });
        }
    }
}
