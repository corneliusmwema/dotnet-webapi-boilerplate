using System.Net;

namespace FSH.Framework.Application.Exceptions;
public class NotFoundException : FshException
{
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}

