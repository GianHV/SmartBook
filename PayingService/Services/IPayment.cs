using PayingService.Models;

namespace PayingService.Services
{
    public interface IPayment
    {
        Task<PaymentResponse> ProcessTransaction(PaymentRequest request);
        Task<bool> CallBack(int orderCode); 
    }
}
