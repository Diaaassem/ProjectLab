using Microsoft.AspNetCore.Mvc;
using ProjectLab.Filters;

namespace ProjectLab.Controllers
{
    [ExceptionHandleFilter]
    public class ExceptionTestController : Controller
    {
        [LoggingHandlingFilter]
        public IActionResult Index()
        {
            throw new NotImplementedException();
        }
    }
}
