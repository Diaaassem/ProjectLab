using Microsoft.AspNetCore.Mvc;
using ProjectLab.Cookies;
using ProjectLab.Services;

namespace ProjectLab.Controllers
{
    public class CookieController : Controller
    {
        private readonly ICookieService _cookieService;
        public CookieController(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        // Quick GET for testing (use POST in real scenarios)
        // /Cookie/Set?key=theme&value=dark
        public IActionResult Set(string key, string value, int days = 30)
        {
            if (string.IsNullOrEmpty(key)) return BadRequest("key is required");
            _cookieService.Set(key, value, days);
            return RedirectToAction("Index", "Student");
        }

        // Read cookie: /Cookie/Get?key=theme
        public IActionResult Get(string key)
        {
            if (string.IsNullOrEmpty(key)) return BadRequest("key is required");
            var v = _cookieService.Get(key);
            return Content(v ?? string.Empty);
        }

        // Delete cookie: /Cookie/Delete?key=theme
        public IActionResult Delete(string key)
        {
            if (string.IsNullOrEmpty(key)) return BadRequest("key is required");
            _cookieService.Delete(key);
            return RedirectToAction("Index", "Student");
        }

        // Protected cookie example
        public IActionResult SetProtected(string key, string value, int days = 30)
        {
            if (string.IsNullOrEmpty(key)) return BadRequest("key is required");
            _cookieService.SetProtected(key, value, days);
            return RedirectToAction("Index", "Student");
        }

        public IActionResult GetProtected(string key)
        {
            if (string.IsNullOrEmpty(key)) return BadRequest("key is required");
            var v = _cookieService.GetProtected(key);
            return Content(v ?? string.Empty);
        }
    }
}