using WebApi.Exceptions.Common;

namespace WebApi.Exceptions;

public sealed class AlreadyExistsException : ApiException
{
    public AlreadyExistsException(string message) : base(message)
    {
    }

    public AlreadyExistsException()
    {
    }

    public AlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public override int StatusCode => 403;
}
