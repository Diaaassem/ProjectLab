using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectLab.Models;

namespace ProjectLab.Filters
{
    public class DepartmentLocationFilter : ActionFilterAttribute
    {
        /*Check if the department city is USA or EG then accept to add it otherwise add validation error*/
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("department"))
            {
                var department = context.ActionArguments["department"] as Department;
                if (department != null)
                {
                    if (string.IsNullOrEmpty(department.City))
                    {
                        context.ModelState.AddModelError("City", "City is required.");
                        var controller = (Controller)context.Controller;
                        context.Result = controller.View("AddDepartment", department);
                        return;
                    }
                    
                    // Check if city is either USA or EG (case-insensitive)
                    var city = department.City.Trim().ToUpper();
                    if (city != "USA" && city != "EG")
                    {
                        context.ModelState.AddModelError("City", "Department city must be either USA or EG.");
                        var controller = (Controller)context.Controller;
                        context.Result = controller.View("AddDepartment", department);
                        return;
                    }
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
