using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizzApp.Data.DbContext;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.Web.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace QuizzApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User , Admin")]
        public IActionResult Privacy()
        {
            if (User.FindFirstValue(ClaimTypes.NameIdentifier)!= null)
            {
                var UrID = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var courses = _context.Courses.Where(c => c.UserId == UrID).ToList();
                return View(courses);
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "User , Admin")]
        public IActionResult DeleteCourse(string courseId)
        {
            if (courseId != null)
            {
                var course = _context.Courses.FirstOrDefault(c => c.Id == new Guid(courseId));
                if (course.UserId == new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                {
                    var tests = _context.TestExams.Where(t => t.CourseId == null).ToList();
                    _context.TestExams.RemoveRange(tests);
                    _context.Courses.Remove(course);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Privacy));
        }

        [Authorize(Roles = "User , Admin")]
        public IActionResult ListQuestions(Guid id)
        {
            var questions = _context.Questions.Where(c => c.CourseId == id).ToList();
            TempData["id"] = id;
            return View(questions);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
