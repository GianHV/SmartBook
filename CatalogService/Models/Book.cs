namespace CatalogService.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int? GenereId { get; set; }
        public string? Publisher { get; set; }
        public string? Language { get; set; }
        public int? PageNumber { get; set; }
        public int? YearPublished { get; set; }
        public int? CopiesTotal { get; set; }
        public int? CopiesAvailable { get; set; }
    }
    public class BookDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int GenereId { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int PageNumber { get; set; }
        public int YearPublished { get; set; }
        public int CopiesTotal { get; set; }
        public int CopiesAvailable { get; set; }
    }
}
