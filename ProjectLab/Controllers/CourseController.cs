using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectLab.Data;
using ProjectLab.Models;

namespace ProjectLab.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult getAll()
        {
            var courses = _context.Courses
                .Include(c => c.Registrations)
                    .ThenInclude(r => r.Student)    
                .Include(c => c.TeachCourses)
                    .ThenInclude(tc => tc.Instructor)
                .ToList();
            return View(courses);
        }

        public IActionResult Details(int id)
        {
            var course = _context.Courses
                .Include(c => c.Registrations)
                    .ThenInclude(r => r.Student)
                .Include(c => c.TeachCourses)
                    .ThenInclude(tc => tc.Instructor)
                .FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult AddCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return RedirectToAction("getAll");
            }
            return View(course);
        }

        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("getAll");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Update(course);
                _context.SaveChanges();
                return RedirectToAction("getAll");
            }
            return View(course);
        }

        public IActionResult IsCourseNameUnique(string name, int id)
        {
            bool isUnique = !_context.Courses.Any(c => c.Name == name && c.Id != id);
            return Json(isUnique);
        }
    }
}
