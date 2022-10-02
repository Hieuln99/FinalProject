//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using QuizzApp.Data.DbContext;
//using QuizzApp.Data.Entities;
//using QuizzApp.Repository.Infrastructures;
//using QuizzApp.VModels.Results;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace QuizzApp.Web.Areas.Admin.Controllers
//{

//    public class ResultsController : AdminController
//    {
//        private readonly AppDbContext _context;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;

//        public ResultsController(AppDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//            _context = context;
//        }

//        // GET: Admin/Results
//        public async Task<IActionResult> Index()
//        {
//            var results =  await _unitOfWork.ResultRepository.GetAll().Include(r => r.Question).ToListAsync();
//            var resultVM = _mapper.Map<IList<Result>, IList<ResultVModel>>(results);
//            return View(resultVM);
//        }

//        // GET: Admin/Results/Details/5
//        public async Task<IActionResult> Details(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var result = await _unitOfWork.ResultRepository.GetAll()
//                .Include(r => r.Question)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            var resultVM = _mapper?.Map<Result,ResultVModel>(result);
//            if (resultVM == null)
//            {
//                return NotFound();
//            }

//            return View(resultVM);
//        }

//        // GET: Admin/Results/Create
//        public IActionResult Create()
//        {
//            ViewData["QuestionId"] = new SelectList(_unitOfWork.QuestionRepository.GetAll(), "Id", "QuestionName");
//            return View();
//        }

//        // POST: Admin/Results/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create([Bind("Id,AnswerText,QuestionId")] ResultModel result)
//        {
//            if (ModelState.IsValid)
//            {
//                result.Id = Guid.NewGuid();
//                var newResult = _mapper.Map<ResultModel, Result>(result);
//                _unitOfWork.ResultRepository.Add(newResult);
//                _unitOfWork.SaveChanges();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["QuestionId"] = new SelectList(_unitOfWork.QuestionRepository.GetAll(), "Id", "QuestionName", result.QuestionId);
//            return View(result);
//        }

//        // GET: Admin/Results/Edit/5
//        public IActionResult Edit(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var result = _unitOfWork.ResultRepository.GetById(id);
//            var resultVM = _mapper.Map<Result, ResultModel>(result);
//            if (resultVM == null)
//            {
//                return NotFound();
//            }
//            ViewData["QuestionId"] = new SelectList(_unitOfWork.QuestionRepository.GetAll(), "Id", "QuestionName", result.QuestionId);
//            return View(resultVM);
//        }

//        // POST: Admin/Results/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(Guid id, [Bind("Id,AnswerText,QuestionId")] ResultModel result)
//        {
//            if (id != result.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    var newResult = _mapper.Map<ResultModel, Result>(result);
//                    _unitOfWork.ResultRepository.Update(newResult);
//                    _unitOfWork.SaveChanges();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ResultExists(result.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["QuestionId"] = new SelectList(_unitOfWork.QuestionRepository.GetAll(), "Id", "QuestionName", result.QuestionId);
//            return View(result);
//        }

//        // GET: Admin/Results/Delete/5
//        public async Task<IActionResult> Delete(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var result = await _unitOfWork.ResultRepository.GetAll()
//                .Include(r => r.Question)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            var newResult = _mapper.Map<Result, ResultVModel>(result);

//            if (newResult == null)
//            {
//                return NotFound();
//            }

//            return View(newResult);
//        }

//        // POST: Admin/Results/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed(Guid id)
//        {
//            var result = _unitOfWork.ResultRepository.GetById(id);
//            _unitOfWork.ResultRepository.Remove(result);
//            _unitOfWork.SaveChanges();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ResultExists(Guid id)
//        {
//            return _context.Results.Any(e => e.Id == id);
//        }
//    }
//}
