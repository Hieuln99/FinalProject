using Microsoft.AspNetCore.Mvc;
using QuizzApp.Data.DbContext;
using QuizzApp.Repository.Infrastructures;
using System.Security.Claims;

namespace QuizzApp.Web.Areas.UserArea.Controllers
{
    public class AccountController : UserController
    {
        private IUnitOfWork _IUnitofWork;
        private AppDbContext _Context;
        public AccountController(IUnitOfWork iunitofwork, AppDbContext context)
        {
            _IUnitofWork = iunitofwork;
            _Context = context;
        }
        public IActionResult Detail()
        {
            var user = _Context.Users.FindAsync(User.Identity.Name);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(user);
        }
    }
}
