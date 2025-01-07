using Common.Base;
using Newtonsoft.Json.Linq;
using RecordService.Models;

namespace RecordService.Alternative
{
    public class PayingService : HttpService, IPayingService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string payingUrl;
        public PayingService(
            IHttpClientFactory clientFactory,
            IConfiguration configuration)
            : base(clientFactory)
        {
            _clientFactory = clientFactory;
            payingUrl = configuration.GetValue<string>("ServiceUrls:payingAPI");
        }
        public Task<T> UpdateMoney<T>(PaymentDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = dto,
                Url = payingUrl + "/api/Paying",
                Token = token,
            });
        }
    }
}
