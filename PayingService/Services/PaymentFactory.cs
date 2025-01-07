using PayingService.Models;

namespace PayingService.Services
{
    public class PaymentFactory
    {
        public static IPayment CreateInstance(
                                            string role,
                                            IConfiguration config,
                                            string type = null)
        {
            if (role == "admin" || role == "employee")
            {
                return new PayInCash(config);
            }
            else
            {
                return new MyPayOS(config);
            }
        }
    }
}
