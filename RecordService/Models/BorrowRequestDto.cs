namespace RecordService.Models
{
    public class BorrowRequestDto
    {
        public string userId {  get; set; }
        public int bookId { get; set; }
        public DateTime dueDate { get; set; }
    }
}
