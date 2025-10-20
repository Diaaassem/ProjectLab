using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ProjectLab.Filters
{
    public class LoggingHandlingFilter : ActionFilterAttribute
    {
        Stopwatch stopwatch = new Stopwatch();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
             stopwatch.Start();
             Console.WriteLine("Action execution started.");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch.Stop();
            var elapsed = stopwatch.Elapsed;            
            Console.WriteLine($"Action executed in: {elapsed.TotalMilliseconds} ms");
        }


    }
}
