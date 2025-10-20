using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectLab.Filters
{
    public class ExceptionHandleFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ContentResult result = new ContentResult
            {
                Content = $"An error occurred: {context.Exception.Message}",
                ContentType = "text/plain",
                StatusCode = 500
            };
            context.Result = result;
        }
    }
}
