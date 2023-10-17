using System.Net;

namespace FSH.Framework.Application.Exceptions;

public class InternalServerException : FshException
{
    public InternalServerException(string message)
        : base(message, HttpStatusCode.InternalServerError)
    {
    }
}