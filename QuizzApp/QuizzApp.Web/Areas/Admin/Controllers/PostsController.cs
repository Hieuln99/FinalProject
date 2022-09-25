
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.VModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizzApp.Web.Areas.Admin.Controllers
{

    public class PostsController : AdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;
        public PostsController(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        [Area(areaName: "Admin")]
        public IActionResult Index()
        {
            var listPost = _unitOfWork.PostRepository.GetAll();
            return View(listPost);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(i => new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.Id.ToString()
            });
            ViewBag.PublishedList = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "True", Value = true.ToString(), Selected=true },
                new SelectListItem() { Text = "False", Value = false.ToString(), Selected=false },
            };

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(CreatePostVM request)
        {

            if (ModelState.IsValid)
            {

                var post = new Post()
                {
                    Title = request.Title,
                    Content = request.Content,
                    CategoryId = request.CategoryId,
                    Description = request.Description,

                };
                this._unitOfWork.PostRepository.Add(post);
                _unitOfWork.SaveChanges();
                TempData["success"] = "Post created successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Post created error";
            ViewBag.CategoryList = _unitOfWork.CategoryRepository.GetAll2().Select(i => new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.Id.ToString()
            });
            ViewBag.PublishedList = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "True", Value = true.ToString(), Selected=true },
                new SelectListItem() { Text = "False", Value = false.ToString(), Selected=false },
            };
            return View(request);
        }

        //GET
        public IActionResult Edit(Guid id)
        {

            if (id.ToString() == null)
            {
                return NotFound();
            }
            var PostFromDbFirst = _unitOfWork.PostRepository.GetById(id);
            if (PostFromDbFirst == null)
            {
                return NotFound();
            }
            UpdatePostVM updatePostVM = new UpdatePostVM()
            {
                Post = PostFromDbFirst,
                //CategoryList = new System.Web.Mvc.SelectList(_unitOfWork.CategoryRepository.GetAll2(), "CategoryId", "Category", PostFromDbFirst.Category)
                CategoryList = new SelectList(_unitOfWork.CategoryRepository.GetAll2(), "CategoryId", "Category", PostFromDbFirst.Category)
            };
            ViewBag.PublishedList = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "True", Value = true.ToString(), Selected=true },
                new SelectListItem() { Text = "False", Value = false.ToString(), Selected=false },
            };
            return View(updatePostVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(UpdatePostVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.PostRepository.Update(obj.Post);
                _unitOfWork.SaveChanges();
                TempData["success"] = "Post updated successfully";
                return RedirectToAction("Index");
            }

            //obj.CategoryList = new System.Web.Mvc.SelectList(_unitOfWork.CategoryRepository.GetAll2(), "CategoryId", "Cate", obj.Post.CategoryId);
            obj.CategoryList = new SelectList(_unitOfWork.CategoryRepository.GetAll2(), "CategoryId", "Cate", obj.Post.CategoryId);
            TempData["error"] = "Post created error";
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            var productList = _unitOfWork.PostRepository.GetAll2();
            switch (status)
            {
                case "latest":
                    productList = _unitOfWork.PostRepository.GetLatestPost(10);
                    break;
                case "mostViewed":
                    productList = _unitOfWork.PostRepository.GetMostViewedPost(10);
                    break;
                case "mostInteresting":
                    productList = _unitOfWork.PostRepository.GetHighestPosts(10);
                    break;
                default:
                    break;
            }
            return Json(new { data = productList });
        }

        //POST

        public IActionResult Delete(Guid? id)
        {
            var obj = _unitOfWork.PostRepository.GetById(id.Value);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.PostRepository.Remove(obj);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");

        }
        #endregion

    }
}
