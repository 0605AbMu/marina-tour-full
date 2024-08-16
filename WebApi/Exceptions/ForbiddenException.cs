using WebApi.Exceptions.Common;

namespace WebApi.Exceptions;

public sealed class ForbiddenException : ApiException
{
    public ForbiddenException(string message) : base(message)
    {
    }

    public ForbiddenException()
    {
    }

    public ForbiddenException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public override int StatusCode => 403;
}
