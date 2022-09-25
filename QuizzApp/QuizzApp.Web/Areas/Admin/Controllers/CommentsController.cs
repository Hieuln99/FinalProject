using Microsoft.AspNetCore.Mvc;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using System;
using System.Linq;

namespace QuizzApp.Web.Areas.Admin.Controllers
{
    
    public class CommentsController : AdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;
        public CommentsController(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult Index()
        {
            var listComments = _unitOfWork.CommentRepository.GetAll();
            return View(listComments);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //Comment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Comment request)
        {

            if (ModelState.IsValid)
            {
                this._unitOfWork.CommentRepository.Add(request);
                _unitOfWork.SaveChanges();
                TempData["success"] = "Comment created successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Comment created error";
            return View(request);
        }

        //GET
        public IActionResult Edit(Guid id)
        {

            if (id.ToString() == null)
            {
                return NotFound();
            }
            var CommentFromDbFirst = _unitOfWork.CommentRepository.GetById(id);
            if (CommentFromDbFirst == null)
            {
                return NotFound();
            }

            return View(CommentFromDbFirst);
        }

        //Comment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Comment obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CommentRepository.Update(obj);
                _unitOfWork.SaveChanges();
                TempData["success"] = "Comment updated successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Comment created error";
            return View(obj);
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            var productList = _unitOfWork.CommentRepository.GetAll();
            return Json(new { data = productList });
        }

        //Comment
       
        
        public IActionResult Delete(Guid? id)
        {
            var obj = _unitOfWork.CommentRepository.GetById(id.Value);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CommentRepository.Remove(obj);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");

        }
        #endregion


    }
}
