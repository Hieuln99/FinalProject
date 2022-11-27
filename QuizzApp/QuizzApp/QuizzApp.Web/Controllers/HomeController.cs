using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
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
        private IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
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
            var questions = _context.Questions.Where(c => c.CourseId == id).Include(c => c.Course).ToList();
            TempData["id"] = id;
            return View(questions);
        }

        [Authorize(Roles = "User , Admin")]
        public IActionResult EditCourrse(Guid id)
        {
            var Course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if(Course != null)
            {
                return View(Course);
            }
            TempData["error"] = "Cannot find this course";
            return RedirectToAction(nameof(Privacy));
        }

        [Authorize(Roles = "User , Admin")]
        [HttpPost]
        public IActionResult EditCourrse(Course course)
        {
            var Course = _context.Courses.FirstOrDefault(c => c.Id == course.Id);
            if (Course != null)
            {
                if (ModelState.IsValid)
                {
                    Course.CourseName = course.CourseName;
                    _context.Courses.Update(Course);
                    _context.SaveChanges();
                    TempData["message"] = "Update success!!";
                    return RedirectToAction(nameof(Privacy));
                }
            }
            TempData["error"] = "Cannot update this course";
            return RedirectToAction(nameof(Privacy));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
