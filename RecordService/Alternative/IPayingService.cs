using RecordService.Models;

namespace RecordService.Alternative
{
    public interface IPayingService
    {
        Task<T> UpdateMoney<T>(PaymentDto dto, string token);
    }
}
