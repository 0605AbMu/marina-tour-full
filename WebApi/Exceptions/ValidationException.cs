using WebApi.Exceptions.Common;

namespace WebApi.Exceptions;

public sealed class ValidationException : ApiException
{
    public ValidationException(string message) : base(message)
    {
    }

    public ValidationException()
    {
    }

    public ValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public override int StatusCode => 400;
}
