using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizzApp.Web.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    [Authorize(Roles = "User , Admin")]
    public class UserController : Controller
    {
      
    }
}
