namespace StatisticsService.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public DateTime borrowDate { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime returnDate { get; set; }
        public BorrowStatus status { get; set; }
    }
    public enum BorrowStatus
    {
        BORROW,
        RETURN
    }
}
