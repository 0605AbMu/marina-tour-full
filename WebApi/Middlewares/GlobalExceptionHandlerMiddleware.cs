using System.Net;
using ResultWrapper.Library;
using Serilog;
using WebApi.Exceptions.Common;

namespace WebApi.Middlewares;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
#pragma warning disable CA1031
        catch (Exception e)
#pragma warning restore CA1031
        {
            if (e is ApiException apiException)
            {
                context.Response.StatusCode = apiException.StatusCode;
                await context.Response.WriteAsJsonAsync(
                    Wrapper.ResultFromException(e, (HttpStatusCode)apiException.StatusCode));
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(
                    Wrapper.ResultFromException(e));

                Log.Error("Exception: {0}", e);
            }
        }
        finally
        {
        }
    }
}
