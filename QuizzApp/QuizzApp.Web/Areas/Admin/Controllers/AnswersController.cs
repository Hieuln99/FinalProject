using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzApp.Web.Areas.Admin.Controllers
{
    
    public class AnswersController : AdminController
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnswersController(AppDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        // GET: Answers/Answers
        public async Task<IActionResult> Index()
        {
            var answers = _unitOfWork.AnswerRepository.GetAll()
                .Include(a => a.Question).Include(a => a.Answer)
                .OrderBy(q=>q.Question);
            return View(await answers.ToListAsync());
        }

        // GET: Answers/Answers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.TestQuestionAnswers
                //.Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answers/Answers/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "QuestionName");
            return View();
        }

        // POST: Answers/Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnswerText,UserName,QuestionId")] TestQuestionAnswer answer)
        {
            if (ModelState.IsValid)
            {
                answer.Id = Guid.NewGuid();
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "QuestionName", answer.QuestionId);
            return View(answer);
        }

        // GET: Answers/Answers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.TestQuestionAnswers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "QuestionName", answer.QuestionId);
            return View(answer);
        }

        // POST: Answers/Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AnswerText,UserName,QuestionId")] TestQuestionAnswer answer)
        {
            if (id != answer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.Id))
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
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "QuestionName", answer.QuestionId);
            return View(answer);
        }

        // GET: Answers/Answers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.TestQuestionAnswers
                //.Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var answer = await _context.TestQuestionAnswers.FindAsync(id);
            _context.TestQuestionAnswers.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerExists(Guid id)
        {
            return _context.TestQuestionAnswers.Any(e => e.Id == id);
        }
    }
}
