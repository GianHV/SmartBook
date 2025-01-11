namespace RecordService.Models
{
    public class PaymentDto
    {
        public string userId { get; set; } = string.Empty;
        public int amount { get; set; }
        public int paymentType { get; set; } = 1;
        public string description { get; set; } = string.Empty;
    }
}
