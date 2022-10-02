using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        //[HttpGet]
        //public IActionResult Find(string stringSearch, int pageIndex = 1)

        //{
        //    int pageSize = 6;
        //    var newCourse = _unitOfWork.CourseRepository.GetAll()
        //                                                .Where(c => c.CourseName
        //                                                .Contains(stringSearch))
        //                                                .ToList();
        //    ViewBag.StringSearch = stringSearch;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.PageIndex = pageIndex;
        //    ViewBag.PageNumber = newCourse.Count() / 6 + 1;

        //    if (stringSearch == null)
        //    {
        //        return NotFound();
        //    }
        //    var listDisplay = newCourse.Skip(pageSize * (pageIndex - 1))
        //                               .Take(pageSize).ToList();
        //    var listCourses = _mapper.Map<IList<Course>, IList<CourseVModel>>(listDisplay); 

        //    return View(listCourses);
        //}

        public IActionResult Delete(Guid id)
        {
            var course = _unitOfWork.CourseRepository.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            _unitOfWork.CourseRepository.Remove(course);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
