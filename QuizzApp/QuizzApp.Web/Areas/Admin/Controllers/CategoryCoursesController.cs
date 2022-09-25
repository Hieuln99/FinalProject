using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using System;
using System.Linq;

namespace QuizzApp.Web.Areas.Admin.Controllers
{

    public class CategoryCoursesController : AdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _maps;

        public CategoryCoursesController(IUnitOfWork unitOfWork, IMapper maps)
        {
            _maps = maps;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var listCategoryCourses = _unitOfWork.CategoryCourseRepository.GetAll().ToList();
            return View(listCategoryCourses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryCourse categoryCourse)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryCourseRepository.Add(categoryCourse);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryCourse);
        }

        public IActionResult Edit(Guid id)
        {
            var categoryCourse = _unitOfWork.CategoryCourseRepository.GetById(id);
            if (categoryCourse == null)
            {
                return NotFound();
            }
            return View(categoryCourse);
        }

        [HttpPost]
        public IActionResult Edit(CategoryCourse categoryCourse)
        {
            _unitOfWork.CategoryCourseRepository.Update(categoryCourse);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            var categoryCourse = _unitOfWork.CategoryCourseRepository.GetById(id);
            if (categoryCourse == null)
            {
                return NotFound();
            }
            _unitOfWork.CategoryCourseRepository.Remove(categoryCourse);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
