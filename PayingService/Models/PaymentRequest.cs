namespace PayingService.Models
{
    public class PaymentRequest
    {
        public string userId { get; set; } = string.Empty;
        public int amount { get; set; }
        public string description { get; set; } = string.Empty;
        public PaymentType paymentType { get; set; }
        // pay in cash, vietqr, vnpay , ...
        public string transactionMethod { get; set; } = string.Empty;
    }

    public enum PaymentType
    {
        DEPOSIT,
        BORROW,
        COMPENSATION
    }
}
