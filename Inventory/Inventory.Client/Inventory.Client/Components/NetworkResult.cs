namespace Inventory.Client.Components
{
    using System.Net;

    public class NetworkResult
    {
        public bool Success { get; }

        public WebExceptionStatus Status { get; }

        public HttpStatusCode StatusCode { get; }

        public NetworkResult(bool success, WebExceptionStatus status, HttpStatusCode statusCode)
        {
            Success = success;
            Status = status;
            StatusCode = statusCode;
        }
    }

    public class NetworkResult<T> : NetworkResult
    {
        public T Result { get; private set; }

        public NetworkResult(bool success, WebExceptionStatus status, HttpStatusCode statusCode, T result)
            : base(success, status, statusCode)
        {
            Result = result;
        }
    }
}
