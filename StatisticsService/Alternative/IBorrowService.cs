namespace StatisticsService.Alternative
{
    public interface IBorrowService
    {
        Task<T> GetBorrow<T>(string token);
    }
}
