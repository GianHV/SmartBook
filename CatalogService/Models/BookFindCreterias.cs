using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Models
{
    public class BookFindCreterias : Paging
    {
        public string? Title { get; set; } = null;
        public string? Author { get; set; } = null;
        public int? GenreId { get; set; } = null;
    }

    public class Paging
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = int.MaxValue;
    }
}
