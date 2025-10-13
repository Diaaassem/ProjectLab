using Microsoft.AspNetCore.Mvc;
using ProjectLab.Data;
using ProjectLab.Models;

namespace ProjectLab.Controllers
{
    public class DepartmentController : Controller
    {
        AppDbContext context = new AppDbContext();
        public IActionResult getAll()
        {
            var depts = context.Departments.ToList();
            return View(depts);
        }

        public IActionResult DetailsById(int id)
        {
            var dept = context.Departments.SingleOrDefault(d => d.DeptId ==  id);
            return View(dept);
        }

        public IActionResult DetailsByName(string name)
        {
            var dept = context.Departments.SingleOrDefault(d => d.Name == name);
            return View(dept);
        }

        public IActionResult AddDepartment()
        {
            return View();
        }

        public IActionResult AddNewDept(Department department)
        {
            if(!ModelState.IsValid)
            {
                return View("AddDepartment");
            }
            context.Departments.Add(department);
            context.SaveChanges();
            return RedirectToAction("getAll");
        }

    }
}
