using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Account;
using QuizzApp.Data.DbContext;
using QuizzApp.Repository.Infrastructures;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace QuizzApp.Web.Areas.UserArea.Controllers
{
    public class AccountController : UserController
    {
        private IUnitOfWork _IUnitofWork;
        private AppDbContext _Context;
        private readonly IMapper _mapper;

        public AccountController(IUnitOfWork iunitofwork, AppDbContext context, IMapper mapper)
        {
            _IUnitofWork = iunitofwork;
            _Context = context;
            _mapper = mapper;   
        }
        public IActionResult Detail()
        {
            //var user = _Context.Users.FindAsync(User.Identity.Name);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _Context.Users.FirstOrDefault(u => u.Id == new Guid(userId));
            var MapAcc = _mapper.Map<UserViewModel>(user);
            return View(MapAcc);
        }
        
        [HttpPost]
        public IActionResult Update(UserViewModel userVM)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _Context.Users.FirstOrDefault(u => u.Id == new Guid(userId));
           
           if(user is not null)
            {
                user.Email = userVM.Email;
                user.PhoneNumber = userVM.PhoneNumber;
                user.UserName = userVM.UserName;
                if (userVM.ImagePath is not null)
                {
                    var path = userVM.ImagePath.FileName.Split('.');
                    var realPath = path[path.Length - 1];
                    user.Extend = "image/" + realPath;
                    user.Image = ConvertToBytes(userVM.ImagePath);
                }
                user.Location = userVM.Location;
                _Context.SaveChanges();
            }
            TempData["message"] = "Update success!";
            return RedirectToAction(nameof(Detail));
        }

        [HttpGet]
        public IActionResult RetriveImg(Guid id)
        {
            var acc = _Context.Users.FirstOrDefault(u => u.Id == id);
            var img = acc.Image;
            return File(img, acc.Extend);
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
