using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResultWrapper.Library;

namespace WebApi.Filters;

public class ModelValidationFilter : IAsyncActionFilter
{
     public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
     {
          if (context.ModelState.IsValid)
          {
               await next();
               return;
          }

          context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
          context.Result =
               new ObjectResult(Wrapper.ResultFromModelState(context.ModelState, new ValidationException()));
     }
}