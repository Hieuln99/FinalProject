using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using System;
using System.Linq;

namespace QuizzApp.Web.Areas.Admin.Controllers
{

    public class CategoriesController : AdminController
    {
        private readonly IUnitOfWork _unitOfWork;

        public readonly IMapper _maps;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper maps)
        {
            _maps = maps;
            _unitOfWork = unitOfWork;
        }




        public IActionResult Index()
        {
            var listCategories = _unitOfWork.CategoryRepository.GetAll().ToList();
            return View(listCategories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult ListCourseOfCategory(Guid id)
        {
            var courses = _unitOfWork.CategoryRepository.GetCourseOfCategory(id);
            ViewBag.CategoryName = _unitOfWork.CategoryRepository.GetById(id).CategoryName;
            return View(courses);
        }

        public IActionResult Edit(Guid id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(Guid categoryId)
        {
            var category = _unitOfWork.CategoryRepository.GetById(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.CategoryRepository.Remove(category);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}