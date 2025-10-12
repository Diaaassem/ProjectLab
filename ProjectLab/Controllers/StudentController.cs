using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectLab.Data;

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
            var student = _context.Students.FirstOrDefault(s => s.SSN == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
    }
}
