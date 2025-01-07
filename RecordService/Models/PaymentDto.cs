namespace RecordService.Models
{
    public class PaymentDto
    {
        public string userId { get; set; } = string.Empty;
        public int amount { get; set; }
        public string description { get; set; } = string.Empty;
    }
}
