using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Account;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace QuizzApp.Web.Areas.Admin.Controllers
{
    public class AccountController : AdminController
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public AccountController(AppDbContext context, IUnitOfWork unitOfWork, IMapper  mapper)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var accounts = _context.Users.ToList();
            return View(accounts);
        }

        public IActionResult Edit(Guid id)
        {
            var account = _context.Users.FirstOrDefault(x => x.Id == id);
            var MapAcc = _mapper.Map<UserViewModel>(account);
            return View(MapAcc);
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel user)
        {
            try
            {
                
                var UpdateUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                if (UpdateUser != null)
                {
                    UpdateUser.Email = user.Email;
                    UpdateUser.Location = user.Location;
                    UpdateUser.PhoneNumber = user.PhoneNumber;
                    
                    if (user.ImagePath is not null)
                    {
                        var img = ConvertToBytes(user.ImagePath);
                        var path = user.ImagePath.FileName.Split('.');
                        var realPath = path[path.Length - 1];
                        user.Extend = "image/" + realPath; 
                        UpdateUser.Image = img;
                        UpdateUser.Extend = "image/" + realPath;
                    }

                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception)
            {
            }

            return View(user);
        }
        [HttpGet]
        public IActionResult Detail(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id==id);
            var MapAcc = _mapper.Map<UserViewModel>(user);
            return View(MapAcc);
        }

        
        public IActionResult Delete(Guid id)
        {
            if(id != new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);
                _context.Users.Remove(user);
                _context.SaveChanges();
                TempData["message"] = "Delete success!!!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Can not delete admin account!!!";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult RetriveImg(Guid id)
        {
            var acc = _context.Users.FirstOrDefault(u => u.Id==id);
            var img = acc.Image;
            return File(img,acc.Extend);
        }
        public byte[] ConvertToBytes(IFormFile img)
        {
            byte[] bytes = null;
            BinaryReader reader = new BinaryReader(img.OpenReadStream());
            bytes = reader.ReadBytes((int)img.Length);
            return bytes;
        }
    }
}
