namespace PayingService.Models
{
    public class PaymentResponse
    {
        public string status {  get; set; } = "PENDING"; // PAID 
        public string redirectUrl { get; set; } = string.Empty;
        public static readonly PaymentResponse Success = new PaymentResponse { status = "PAID", redirectUrl = "" };
        public static readonly PaymentResponse Pending = new PaymentResponse { status = "PENDING", redirectUrl = "" };

    }
}
