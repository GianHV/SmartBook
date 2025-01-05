namespace Common.Base
{
    public interface IHttpService
    {
        Task<T> SendAsync<T>(APIRequest apiRequest, bool withBeaer = true);
    }
}
