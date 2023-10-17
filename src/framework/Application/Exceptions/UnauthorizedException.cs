using System.Net;

namespace FSH.Framework.Application.Exceptions;

public class UnauthorizedException : FshException
{
    public UnauthorizedException(string message)
       : base(message, HttpStatusCode.Unauthorized)
    {
    }
}