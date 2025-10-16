using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectLab.Data;
using ProjectLab.Models;
using ProjectLab.Models.ViewModels;

namespace ProjectLab.Controllers
{
    public class StudentController : Controller
    {
        AppDbContext _context = new AppDbContext();

        public IActionResult getAll()
        {
            var data = _context.Students.ToList();
            return View(data);
        }

        public IActionResult Details(int id)
        {
            var student = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Registrations)
                    .ThenInclude(r => r.Course)
                .FirstOrDefault(s => s.SSN == id);
            
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public IActionResult StdDetailsVM(int id)
        {
            var student = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Registrations)
                    .ThenInclude(r => r.Course)
                .FirstOrDefault(s => s.SSN == id);
            if (student == null)
            {
                return NotFound();
            }
            var studentDetailsVM = new StudentDetailsVM
            {
                StudentName = student.Name,
                DepartmentName = student.Department.Name,
                Courses = student.Registrations.Select(r => new CourseGradeVM
                {
                    CourseId = r.Course.Id,
                    CourseName = r.Course.Name,
                    CourseTopic = r.Course.Topic,
                    CourseDegree = r.Course.Degree,
                    CourseMinDegree = r.Course.MinDegree,
                    Grade = r.Grade
                }).ToList()
            };
            return View(studentDetailsVM);
        }

        public IActionResult AddStudent()
        {
            var departments = _context.Departments.ToList();
            ViewBag.Departments = departments;
            return View();
        }

        public IActionResult AddNewStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("getAll");
        }

        public IActionResult Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.SSN == id);
            if (student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("getAll");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.SSN == id);
            if (student == null)
            {
                return NotFound();
            }
            var departments = _context.Departments.ToList();
            ViewBag.Departments = departments;
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            _context.Students.Update(student);
            _context.SaveChanges();
            return RedirectToAction("getAll");
        }
    }
}
