using System.Net.Mime;

namespace Common.Base
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
        public ContentType ContentType { get; set; } = ContentType.Json;
    }
    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    public enum ContentType
    {
        Json,
        MultipartFormData,
    }
}
