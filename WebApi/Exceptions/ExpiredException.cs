using WebApi.Exceptions.Common;

namespace WebApi.Exceptions;

public class ExpiredException : ApiException
{
    public ExpiredException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }

    public ExpiredException()
    {
    }

    public ExpiredException(string message) : base(message)
    {
    }
}
