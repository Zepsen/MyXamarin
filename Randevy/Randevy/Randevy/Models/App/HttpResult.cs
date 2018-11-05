using System.Net;

namespace Randevy.Models.App
{
    public class HttpResult
    {
        public HttpStatusCode HttpStatusCode { get; set; }
    }

    public class HttpResult<T>
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public T Data { get; set; }
    }
}
