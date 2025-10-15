using Microsoft.AspNetCore.Mvc;
using ProjectLab.Data;
using ProjectLab.Models;

namespace ProjectLab.Controllers
{
    public class InstructorController : Controller
    {
        AppDbContext _context = new AppDbContext();
        public IActionResult getAll()
        {
            var data = _context.Instructors.ToList();
            return View(data);
        }

        public IActionResult Details(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        [HttpGet]
        public IActionResult AddInstructor()
        {
            var departments = _context.Departments.ToList();
            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewInstructor(Instructor instructor)
        {
            _context.Instructors.Add(instructor);
            _context.SaveChanges();
            return RedirectToAction("getAll");
        }

        public IActionResult Delete(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }
            _context.Instructors.Remove(instructor);
            _context.SaveChanges();
            return RedirectToAction("getAll");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }
            var departments = _context.Departments.ToList();
            ViewBag.Departments = departments;
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            _context.Instructors.Update(instructor);
            _context.SaveChanges();
            return RedirectToAction("getAll");
        }

    }
}
