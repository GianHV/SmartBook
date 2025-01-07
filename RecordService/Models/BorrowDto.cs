namespace RecordService.Models
{
    public class BorrowDto
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public int bookId { get; set; }
        public BorrowStatus status { get; set; }
    }
}
