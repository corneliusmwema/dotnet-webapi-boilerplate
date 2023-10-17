using System.Net;

namespace FSH.Framework.Application.Exceptions;

public class ConflictException : FshException
{
    public ConflictException(string message)
        : base(message, HttpStatusCode.Conflict)
    {
    }
}