using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProjectLab.Data;
using ProjectLab.Filters;
using ProjectLab.Models;
using ProjectLab.Models.ViewModels;
using ProjectLab.Repos;

namespace ProjectLab.Controllers
{
    public class StudentController : Controller
    {
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IGenericRepository<Department> _departmentRepository;

        public StudentController(IGenericRepository<Student> studentRepository, IGenericRepository<Department> departmentRepository)
        {
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
        }

        [ResultFilterCash]
        public async Task<IActionResult> getAll()
        {
            var data = await _studentRepository.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentRepository.GetWithCustomIncludeAsync(
                s => s.SSN == id,
                query => query
                    .Include(s => s.Department)
                    .Include(s => s.Registrations)
                        .ThenInclude(r => r.Course)
            );

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public async Task<IActionResult> StdDetailsVM(int id)
        {
            var student = await _studentRepository.GetWithCustomIncludeAsync(
                s => s.SSN == id,
                query => query
                    .Include(s => s.Department)
                    .Include(s => s.Registrations)
                        .ThenInclude(r => r.Course)
            );

            if (student == null)
            {
                return NotFound();
            }

            var studentDetailsVM = new StudentDetailsVM
            {
                StudentName = student.Name,
                DepartmentName = student.Department?.Name,
                Courses = student.Registrations?.Select(r => new CourseGradeVM
                {
                    CourseId = r.Course?.Id ?? 0,
                    CourseName = r.Course?.Name,
                    CourseTopic = r.Course?.Topic,
                    CourseDegree = r.Course?.Degree ?? 0,
                    CourseMinDegree = r.Course?.MinDegree ?? 0,
                    Grade = r.Grade
                }).ToList() ?? new List<CourseGradeVM>()
            };

            return View(studentDetailsVM);
        }

        public async Task<IActionResult> AddStudent()
        {
            var departments = await _departmentRepository.GetAllAsync();
            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStudent(Student student)
        {
            if (student.DeptId == null)
            {
                ModelState.AddModelError("DeptId", "Department is required.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _departmentRepository.GetAllAsync();
                return View("AddStudent", student);
            }

            await _studentRepository.AddAsync(student);
            return RedirectToAction("getAll");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            await _studentRepository.DeleteAsync(id);
            return RedirectToAction("getAll");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var departments = await _departmentRepository.GetAllAsync();
            ViewBag.Departments = departments;
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if (student.DeptId == null)
            {
                ModelState.AddModelError("DeptId", "Department is required.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _departmentRepository.GetAllAsync();
                return View(student);
            }

            await _studentRepository.UpdateAsync(student);
            return RedirectToAction("getAll");
        }
    }
}