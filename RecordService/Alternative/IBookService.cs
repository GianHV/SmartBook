using RecordService.Models;

namespace RecordService.Alternative
{
    public interface IBookService
    {
        Task<T> UpdateProduct<T>(BookDto dto, string token);
    }
}
