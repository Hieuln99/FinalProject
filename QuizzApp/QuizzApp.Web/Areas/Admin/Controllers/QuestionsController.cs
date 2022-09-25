using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.VModels.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, User")]
    public class QuestionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionsController(AppDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Questions/Questions
        public IActionResult Index()
        {
            var questions = _unitOfWork.QuestionRepository.GetAll().Include(q => q.Course)
                .OrderBy(q => q.Course);
            return View(questions.ToList());
        }

        // GET: Questions/Questions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _unitOfWork.QuestionRepository.GetAll().Include(q => q.Course)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Questions/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_unitOfWork.CourseRepository.GetAll(), "Id", "CourseName");
            return View();
        }

        // POST: Questions/Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,QuestionName,IsMultiple,CourseId")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.Id = Guid.NewGuid();
                _unitOfWork.QuestionRepository.Add(question);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_unitOfWork.CourseRepository.GetAll(), "Id", "CourseName", question.CourseId);
            return View(question);
        }

        // GET: Questions/Questions/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = _unitOfWork.QuestionRepository.GetById(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_unitOfWork.CourseRepository.GetAll(), "Id", "CourseName", question.CourseId);
            return View(question);
        }

        // POST: Questions/Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,QuestionName,IsMultiple,CourseId")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.QuestionRepository.Update(question);
                    _unitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
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
            ViewData["CourseId"] = new SelectList(_unitOfWork.CourseRepository.GetAll(), "Id", "CourseName", question.CourseId);
            return View(question);
        }

        // GET: Questions/Questions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _unitOfWork.QuestionRepository.GetAll().Include(q => q.Course)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var question = _unitOfWork.QuestionRepository.GetById(id);
            _unitOfWork.QuestionRepository.Remove(question);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        //public IActionResult Find(Guid id)
        //{
        //    List<int> List = new List<int>();
        //    List<Question> newList = new List<Question>();
        //    var questions = _unitOfWork.QuestionRepository.GetAll().Include(q => q.Options).Where(q => q.Course.Id == id).ToList();
        //    var newQues = questions.ToArray();
        //    Random random = new Random();

        //    var quest = random.Next(0, questions.Count());
        //    List.Add(quest);

        //    do
        //    {
        //        quest = random.Next(0, questions.Count());
        //        if (!List.Contains(quest))
        //        {
        //            List.Add(quest);
        //        }
        //    } while (List.Count() < questions.Count());


        //    foreach (var a in List)
        //    {
        //        newList.Add(newQues[a]);
        //    }

        //    var newQuestion = _mapper.Map<IList<Question>, IList<QuestionVModel>>(newList);
        //    TempData["NumberQuestions"] = newQuestion.Count;
        //    return View(newQuestion);
        //}

        public IActionResult Check(IFormCollection form)
        {
            var list = form.ToList();
            int mark = 0;
            list.RemoveAt(form.Count() - 1);

            foreach (var question in list)
            {
                if (_unitOfWork.QuestionRepository.FindByIdString(question.Key).IsMultiple)
                {
                    var listResultTrue = _unitOfWork.OptionRepository.GetAll().Where(o => o.QuestionId.ToString() == question.Key && o.Status == true).ToList();
                    var listOptionOfQuestion = question.Value.ToList();
                    if (listOptionOfQuestion.Count() == listResultTrue.Count())
                    {
                        if (CheckMultiple(listOptionOfQuestion))
                        {
                            mark++;
                        }
                    }
                }
                else
                {
                    var Result = _unitOfWork.OptionRepository.GetAll().Where(o => o.QuestionId.ToString() == question.Key && o.Status == true).FirstOrDefault();
                    if (Result.Id.ToString() == question.Value)
                    {
                        mark++;
                    }
                }
            }

            var questions = TempData["NumberQuestions"].ToString();
            var number = Convert.ToInt32(questions);
            var grade = Math.Round((float)(mark * 10) / number, 2);
            return View(grade);
        }

        private bool QuestionExists(Guid id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }

        public bool CheckMultiple(List<string> listQuestion)
        {
            foreach (var answer in listQuestion)
            {
                if (!_context.Options.FirstOrDefault(o => o.Id.ToString() == answer).Status)
                {
                    return false;
                }
            }
            return true;
        }


    }
}
