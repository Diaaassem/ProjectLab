using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectLab.Data;
using ProjectLab.Models;

namespace ProjectLab.Controllers
{
    public class CourseController : Controller
    {
        AppDbContext context = new AppDbContext();

        public IActionResult getAll()
        {
            var courses = context.Courses
                .Include(c => c.Registrations)
                    .ThenInclude(r => r.Student)    
                .Include(c => c.TeachCourses)
                    .ThenInclude(tc => tc.Instructor)
                .ToList();
            return View(courses);
        }

        public IActionResult Details(int id)
        {
            var course = context.Courses
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
                context.Courses.Add(course);
                context.SaveChanges();
                return RedirectToAction("getAll");
            }
            return View(course);
        }

        public IActionResult Delete(int id)
        {
            var course = context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            context.Courses.Remove(course);
            context.SaveChanges();
            return RedirectToAction("getAll");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = context.Courses.Find(id);
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
                context.Courses.Update(course);
                context.SaveChanges();
                return RedirectToAction("getAll");
            }
            return View(course);
        }
    }
}
