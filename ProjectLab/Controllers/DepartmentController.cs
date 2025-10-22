using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectLab.Data;
using ProjectLab.Filters;
using ProjectLab.Models;

namespace ProjectLab.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult getAll()
        {
            var depts = _context.Departments.ToList();
            return View(depts);
        }

        public IActionResult DetailsById(int id)
        {
            var dept = _context.Departments
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
            var dept = _context.Departments
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

        [HttpPost]
        [DepartmentLocationFilter]
        public IActionResult AddNewDept(Department department)
        {
            if(!ModelState.IsValid)
            {
                return View("AddDepartment", department);
            }
            _context.Departments.Add(department);
            _context.SaveChanges();
            return RedirectToAction("getAll");
        }

        public IActionResult Delete(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept == null)
            {
                return NotFound();
            }
            _context.Departments.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("getAll");
        }

        public IActionResult Edit(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
            return RedirectToAction("getAll");
        }
    }
}
