using WebApi.Exceptions.Common;

namespace WebApi.Exceptions;

public sealed class BadRequestException : ApiException
{
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException()
    {
    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public override int StatusCode => 400;
}
