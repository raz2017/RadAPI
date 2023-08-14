using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RadancyAPI.Controllers.Exceptions;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is ArgumentException argumentException)
        {
            context.Result = new ObjectResult(
                new HttpResponseException
                {
                    Message = context.Exception.Message,
                    Source = context.Exception.Source,
                    ExceptionType = context.Exception.GetType().FullName
                }
            )
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            context.ExceptionHandled = true;
        }
    }
}