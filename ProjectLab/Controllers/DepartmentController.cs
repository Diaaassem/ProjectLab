using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var dept = context.Departments
                .Include(d => d.Students)
                .Include(d => d.Instructors)
                .SingleOrDefault(d => d.DeptId == id);

            if (dept == null)
            {
                return NotFound();
            }

            return View(dept);
        }

        public IActionResult DetailsByName(string name)
        {
            var dept = context.Departments
                .Include(d => d.Students)
                .Include(d => d.Instructors)
                .SingleOrDefault(d => d.Name == name);

            if (dept == null)
            {
                return NotFound();
            }

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

        public IActionResult Delete(int id)
        {
            var dept = context.Departments.Find(id);
            if (dept == null)
            {
                return NotFound();
            }
            context.Departments.Remove(dept);
            context.SaveChanges();
            return RedirectToAction("getAll");
        }

        public IActionResult Edit(int id)
        {
            var dept = context.Departments.Find(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            context.Departments.Update(department);
            context.SaveChanges();
            return RedirectToAction("getAll");
        }
    }
}
