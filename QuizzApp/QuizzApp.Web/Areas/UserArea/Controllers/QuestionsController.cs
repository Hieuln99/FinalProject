using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.VModels.Answers;
using QuizzApp.VModels.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace QuizzApp.Web.Areas.UserArea.Controllers
{
   
    public class QuestionsController : UserController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public QuestionsController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
 
        public IActionResult Find(Guid id)
        {
            List<int> List = new List<int>();
            List<Question> newList = new List<Question>();
            var questions = _unitOfWork.QuestionRepository.GetAll()
                .Include(q => q.Options)
                .Where(q => q.Course.Id == id)
                .ToList();

            var newQues = questions.ToArray();
            Random random = new Random();

            if (questions != null)
            {
                var quest = random.Next(0, questions.Count);
                List.Add(quest);
                var Length = questions.Count;

                do
                {
                    quest = random.Next(0, questions.Count);
                    if (!List.Contains(quest))
                    {
                        List.Add(quest);
                    }
                } while (List.Count < 10 ? List.Count < Length : List.Count < 10
                );


            }

            for (int i = 0; i < List.Count; i++)
            {
                if (newQues.Length != 0)
                {
                    newList.Add(newQues[List[i]]);
                }
            }


            if (questions.Count != 0)
            {
                TestExam newTest = new TestExam();
                newTest.Id = Guid.NewGuid();
                newTest.Name = User.Identity.Name;
                string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                newTest.UserId = new Guid(UserId);
                newTest.TakeOn = DateTime.Now;
                newTest.CourseId = questions.FirstOrDefault().CourseId;
                _context.TestExams.Add(newTest); 
                _context.SaveChanges();
                foreach (var question in newList)
                {
                    TestQuestion questionTest = new TestQuestion();
                    questionTest.TestId = Guid.NewGuid();
                    questionTest.QuestionId = question.Id;
                    questionTest.TestExamId = newTest.Id;
                    _context.TestQuestions.Add(questionTest);
                    _context.SaveChanges();
                }
                var newest = (from test in _context.TestExams
                             orderby test.TakeOn descending
                             select test).FirstOrDefault();

                var listQues = (from ques in _context.Questions
                                join questest in _context.TestQuestions
                                on ques.Id equals questest.QuestionId
                                join test in _context.TestExams
                                on questest.TestExamId equals test.Id
                                where new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)) == test.UserId
                                && test.CourseId == id && test.TakeOn == newest.TakeOn
                                select ques).ToList();


                var Questions = _mapper.Map<IList<Question>, IList<QuestionVModel>>(listQues);
                TempData["NumberQuestions"] = Questions.Count;
                return View(Questions);
            }

            var newQuestion = _mapper.Map<IList<Question>, IList<QuestionVModel>>(newList);
            TempData["NumberQuestions"] = newQuestion.Count;
            return View(newQuestion);
        }

        [HttpGet]
        public IActionResult Resume(Guid id)
        {
            var newest = (from test in _context.TestExams
                          orderby test.TakeOn descending
                          select test).FirstOrDefault();
            var questions = (from ques in _context.Questions
                             join questest in _context.TestQuestions
                             on ques.Id equals questest.QuestionId
                             join test in _context.TestExams
                             on questest.TestExamId equals test.Id
                             where new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)) == test.UserId
                             && test.CourseId == id && test.TakeOn == newest.TakeOn && test.Status == false
                             select ques).Include(q => q.Options).ToList();
            var Questions = _mapper.Map<IList<Question>, IList<QuestionVModel>>(questions);
            TempData["NumberQuestions"] = Questions.Count;

            ViewBag.options = (from op in _context.TestQuestionAnswers
                              join tq in _context.TestQuestions
                              on op.TestId equals tq.TestId
                              join te in _context.TestExams
                              on tq.TestExamId equals te.Id
                              where 
                              te.Id == newest.Id  // wrong
                              && te.CourseId == id
                              && new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)) == te.UserId
                              && te.Status == false
                              select op.AnswerId).ToList();
            

            return View(Questions);
        }

        public IActionResult Pause(IFormCollection form)
        {
            var newest = (from test in _context.TestExams
                          orderby test.TakeOn descending
                          select test).FirstOrDefault();

            var listTest = (from testquest in _context.TestQuestions
                            join test in _context.TestExams
                            on testquest.TestExamId equals test.Id
                            where new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)) == test.UserId
                            && test.TakeOn == newest.TakeOn
                            select testquest).FirstOrDefault();

            var newList = form.ToList();
            newList.RemoveAt(form.Count - 1);
           
            var answer = new TestQuestionAnswer();
            foreach (var item in newList)
            {

                foreach (var option in item.Value)
                {
                    answer.Id = Guid.NewGuid();
                    answer.QuestionId = new Guid(item.Key);
                    answer.Summited = DateTime.Now;
                    answer.TestId = listTest.TestId;
                    answer.AnswerId = _context.Options.FirstOrDefault(o => o.Id == new Guid(option)).Id;
                    _unitOfWork.TestAnswers.Add(answer);
                    _unitOfWork.SaveChanges();
                }
            }
            return Ok();
            //return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(new { data= "true"}));
        }

        public IActionResult Check(IFormCollection form)
        {
            var list = form.ToList();
            int mark = 0;
            list.RemoveAt(form.Count - 1);

            foreach (var question in list)
            {
                if (_unitOfWork.QuestionRepository.FindByIdString(question.Key).IsMultiple)
                {
                    var listResultTrue = _unitOfWork.OptionRepository.GetAll().Where(o => o.QuestionId.ToString() == question.Key && o.Status == true).ToList();
                    var listOptionOfQuestion = question.Value.ToList();
                    if (listOptionOfQuestion.Count == listResultTrue.Count)
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

            //AddAnswer(list);

            var test = _context.TestExams.OrderByDescending(t => t.TakeOn).FirstOrDefault(t =>t.UserId == new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            test.Status = true;
            _context.SaveChanges();

            var questions = TempData["NumberQuestions"].ToString();
            var number = Convert.ToInt32(questions);
            var grade = Math.Round((float)(mark * 10) / number, 2);
            return View("Submit",grade);
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


        public IActionResult Submit()
        {
            return View();
        }


        public void AddAnswer(List<KeyValuePair<string,Microsoft.Extensions.Primitives.StringValues>> list)
        {

            foreach (var question in list)
            {
                var options = question.Value.ToList();
                var ques = _unitOfWork.QuestionRepository.FindByIdString(question.Key);
                foreach (var item in options)
                {
                    var answer = new TestQuestionAnswer();
                    //answer.Question = ques;
                    //answer.AnswerText = _unitOfWork.OptionRepository.FindByIdString(item).OptionName;
                    answer.QuestionId = ques.Id;
                    answer.Summited = DateTime.Now;

                    _unitOfWork.AnswerRepository.Add(answer);
                    _unitOfWork.SaveChanges();
                }
            }
        }
    }
}
