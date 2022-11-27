using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.VModels.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizzApp.Web.Areas.UserArea.Controllers
{
    
    public class CoursesController : UserController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CoursesController(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        // GET: User/Courses
        public async Task<IActionResult> Index(int pageIndex = 1)
        {

            int pageSize = 6;
            var courses = await _unitOfWork.CourseRepository.GetAll().ToListAsync();

            ViewBag.PageSize = pageSize;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageNumber = courses.Count() / pageSize + 1;

            if (courses == null)
            {
                return NotFound();
            }
            var listDisplay = courses.Skip(pageSize * (pageIndex - 1))
                                       .Take(pageSize).ToList();

            var coursesVM = _mapper.Map<IList<Course>,IList<CourseVModel>>(listDisplay);
            return View(coursesVM);
        }

        // GET: User/Courses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult CreateCourse()
        {
            return View();
        }

        // POST: User/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseName,CreatedTime")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.Id = Guid.NewGuid();
                course.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("privacy","Home", new { area = "" });
            }
            return View(course);
        }

        [HttpGet]
        public IActionResult Find(string stringSearch, int pageIndex = 1)
        {
            var newCourse = _unitOfWork.CourseRepository.GetAll();
            if (stringSearch == null)
            {
                newCourse.ToList();
            }
            else
            {
                newCourse = newCourse.Where(c => c.CourseName.Contains(stringSearch));
            }
            int pageSize = 6;
            var newnew = newCourse.ToList();
            ViewBag.StringSearch = stringSearch;
            ViewBag.PageSize = pageSize;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageNumber = newnew.Count() / 6 + 1;

           
            var listDisplay = newnew.Skip(pageSize * (pageIndex - 1))
                                       .Take(pageSize).ToList();
            var listCourses = _mapper.Map<IList<Course>, IList<CourseVModel>>(listDisplay);

            return View(listCourses);
        }

        // GET: User/Courses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: User/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CourseName,CreatedTime")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: User/Courses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: User/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(Guid id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        
    }
}
