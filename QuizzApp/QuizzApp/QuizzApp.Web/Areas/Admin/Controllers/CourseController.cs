using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using System;
using System.Linq;
using System.Security.Claims;

namespace QuizzApp.Web.Areas.Admin.Controllers
{

    public class CourseController : AdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        
        public CourseController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var listCourse = _unitOfWork.CourseRepository.GetAll().ToList();
            return View(listCourse);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                course.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _unitOfWork.CourseRepository.Add(course);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public IActionResult Edit(Guid id)
        {
            var course = _unitOfWork.CourseRepository.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            course.CreatedTime = DateTime.Now;
            _unitOfWork.CourseRepository.Update(course);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Request()
        {
            var Requests = _context.ListApproves.Include(u => u.Course).Include(u => u.User).ToList();
            return View(Requests);
        }
         

        [HttpGet]
        public IActionResult Approve(User approve)
        {
            var request = _context.ListApproves.FirstOrDefault(l => l.Unique == approve.Id);
            if(request != null)
            {
                UserCoursePayment payment = new UserCoursePayment();
                payment.UserId = request.UserId;
                payment.CourseId = request.CourseId;
                payment.BuyTime = DateTime.Now;
                _context.UserCoursePayments.Add(payment);
                _context.ListApproves.Remove(request);
                _context.SaveChanges();
            }

            TempData["message"] = "Approved!!!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Cancle(User approve)
        {
           
            var request = _context.ListApproves.FirstOrDefault(a => a.Unique == approve.Id);
            if (request != null)
            {
                _context.ListApproves.Remove(request);
                _context.SaveChanges();
                TempData["message"] = "Remove request success!!!";
            }
            return RedirectToAction(nameof(Request));
        }

        public IActionResult Delete(Guid id)
        {
            var course = _unitOfWork.CourseRepository.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            var tests = _context.TestExams.Where(t => t.CourseId == null).ToList();
            _context.TestExams.RemoveRange(tests);
            _unitOfWork.CourseRepository.Remove(course);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
