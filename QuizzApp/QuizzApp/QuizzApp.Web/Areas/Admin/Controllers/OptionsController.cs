﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Options;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizzApp.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin, User")]
    public class OptionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OptionsController(AppDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        // GET: Admin/Options
        public async Task<IActionResult> Index()
        {
            var appDbContext = _unitOfWork.OptionRepository.GetAll().Include(o => o.Question).OrderBy(o=>o.QuestionId);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Options/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var option = await _unitOfWork.OptionRepository.GetAll()
                .Include(o => o.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }

        // GET: Admin/Options/Create

        public IActionResult Create()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Admin"))
            {
                ViewData["course"] = _unitOfWork.CourseRepository.GetAll().Select(o => new SelectListItem
                {
                    Text = o.CourseName,
                    Value = o.Id.ToString()
                });
                //ViewData["QuestionId"] = new SelectList(_unitOfWork.QuestionRepository.GetAll(), "Id", "QuestionName");
                return View();
            }
            else
            {
                ViewData["course"] = _context.Courses.Where(c => c.UserId == new Guid(userId)).Select(o => new SelectListItem
                {
                    Text = o.CourseName,
                    Value = o.Id.ToString()
                });
                //ViewData["QuestionId"] = new SelectList(_unitOfWork.QuestionRepository.GetAll(), "Id", "QuestionName");
                return View();
            }
        }
        public IActionResult CreateNew(Guid id)
        {
            ViewData["course"] = _context.Courses.Where(o => o.Id == id).Select(o => new SelectListItem
            {
                Text = o.CourseName,
                Value = o.Id.ToString()
            });
            return View(nameof(Create));
        }
        // POST: Admin/Options/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateOptionVM option)
        {
            var Status = new List<bool>(); 
            Status.Add(option.Status1);
            Status.Add(option.Status2);
            Status.Add(option.Status3);
            Status.Add(option.Status4);
            option.Status= Status;
            int mutiple = 0;
            if (!option.CourseId.Equals(new Guid("00000000-0000-0000-0000-000000000000")) && option.Status.Contains(true))
            {
                if (ModelState.IsValid)
                {
                    var tmpoption = new Option();
                    var tmQuestion = new Question();
                    tmQuestion.Id = Guid.NewGuid();
                    var questId = tmQuestion.Id;
                    tmQuestion.CourseId = option.CourseId;
                    tmQuestion.QuestionName = option.QuestionName;
                    for (int i = 0; i < 4; i++)
                    {
                        if(option.Options[i] != null && option.Status[i] == true)
                        {
                            mutiple++;
                        }
                    }
                    //var number = option.Status.Where(s => s is true).Count();
                    if (mutiple == 1)
                    {
                        tmQuestion.IsMultiple = false;
                    }
                    if (mutiple > 1)
                    {
                        tmQuestion.IsMultiple = true;
                    }
                    if(mutiple == 0)
                    {
                        TempData["error"] = "Your question is not valid try input option with status suiltable!";
                        ViewData["course"] = _unitOfWork.CourseRepository.GetAll().Select(o => new SelectListItem
                        {
                            Text = o.CourseName,
                            Value = o.Id.ToString()
                        });
                        ViewData["QuestionId"] = new SelectList(_unitOfWork.QuestionRepository.GetAll(), "Id", "QuestionName");
                        return View(option);
                    }
                    if (option.Options.Where(o => o is null).Count() == 4)
                    {
                        ViewData["course"] = _unitOfWork.CourseRepository.GetAll().Select(o => new SelectListItem
                        {
                            Text = o.CourseName,
                            Value = o.Id.ToString()
                        });
                        ViewData["QuestionId"] = new SelectList(_unitOfWork.QuestionRepository.GetAll(), "Id", "QuestionName");
                        return View(option);
                    }
                    _unitOfWork.QuestionRepository.Add(tmQuestion);
                    _unitOfWork.SaveChanges();

                    for (int i = 0; i < option.Options.Count; i++)
                    {
                        if (option.Options[i] != null)
                        {
                            tmpoption.Id = Guid.NewGuid();
                            tmpoption.Status = option.Status[i];
                            tmpoption.OptionName = option.Options[i];
                            tmpoption.QuestionId = questId;
                            _unitOfWork.OptionRepository.Add(tmpoption);
                            _unitOfWork.SaveChanges();
                        }
                    }
                    TempData["message"] = "Your question created!";
                    return RedirectToAction("Listquestions", "Home", new { area = "", id = option.CourseId });
                }
            }
            TempData["error"] = "Your question is not valid try select at least one option is true!!";
            ViewData["course"] = _unitOfWork.CourseRepository.GetAll().Select(o => new SelectListItem
            {
                Text = o.CourseName,
                Value = o.Id.ToString()
            });
            ViewData["QuestionId"] = new SelectList(_unitOfWork.QuestionRepository.GetAll(), "Id", "QuestionName");
            return View(option);
        }

        // GET: Admin/Options/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var option = _unitOfWork.OptionRepository.GetById(id);
            if (option == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_unitOfWork.QuestionRepository.GetAll(), "Id", "QuestionName", option.QuestionId);
            return View(option);
        }

        // POST: Admin/Options/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,OptionName,QuestionId")] Option option)
        {
            if (id != option.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.OptionRepository.Update(option);
                    _unitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionExists(option.Id))
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
            ViewData["QuestionId"] = new SelectList(_unitOfWork.QuestionRepository.GetAll(), "Id", "QuestionName", option.QuestionId);
            return View(option);
        }

        // GET: Admin/Options/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var option = await _unitOfWork.OptionRepository.GetAll()
                .Include(o => o.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }

        // POST: Admin/Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var option =  _unitOfWork.OptionRepository.GetById(id);
            _unitOfWork.OptionRepository.Remove(option);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool OptionExists(Guid id)
        {
            return _context.Options.Any(e => e.Id == id);
        }
    }
}
