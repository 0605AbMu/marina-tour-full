namespace WebApi.Exceptions.Common;

public abstract class ApiException : Exception
{
    protected ApiException(string message) : base(message)
    {
    }

    protected ApiException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ApiException(Exception exception) : base(exception.Message, exception)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        this.StatusCode = 500;
    }

    protected ApiException()
    {
    }

    public virtual int StatusCode { get; set; }
}
