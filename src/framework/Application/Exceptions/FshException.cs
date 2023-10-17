using System.Net;

namespace FSH.Framework.Application.Exceptions;
public class FshException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public FshException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
