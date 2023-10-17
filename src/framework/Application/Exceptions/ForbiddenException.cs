using System.Net;

namespace FSH.Framework.Application.Exceptions;

public class ForbiddenException : FshException
{
    public ForbiddenException(string message)
        : base(message, HttpStatusCode.Forbidden)
    {
    }
}