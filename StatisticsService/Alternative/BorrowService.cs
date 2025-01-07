
using Common.Base;

namespace StatisticsService.Alternative
{
    public class BorrowService : HttpService, IBorrowService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string bookUrl;
        public BorrowService(
           IHttpClientFactory clientFactory,
           IConfiguration configuration)
           : base(clientFactory)
        {
            _clientFactory = clientFactory;
            bookUrl = configuration.GetValue<string>("ServiceUrls:borrowAPI");
        }
        public Task<T> GetBorrow<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = bookUrl + "/api/Borrow",
                Token = token,
            });
        }
    }
}
