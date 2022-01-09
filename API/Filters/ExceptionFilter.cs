using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Domain;

namespace API.Filters
{
    public class ExceptionFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is GameException exception)
            {
                string message = exception.Message;
                int statusCode = exception.Code;

                context.Result = new JsonResult(new { statusCode, message })
                {
                    StatusCode = statusCode,
                };

                context.ExceptionHandled = true;
            }
        }

    }
}