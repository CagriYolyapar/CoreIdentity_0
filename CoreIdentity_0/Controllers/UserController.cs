using CoreIdentity_0.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentity_0.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //List<AppUser> allUsers = _userManager.Users.ToList(); 

            List<AppUser> nonAdminUsers = _userManager.Users.Where(x => !x.UserRoles.Any(x => x.Role.Name == "Admin")).ToList();


            return View(nonAdminUsers);
        }

        //todo : Create

        public IActionResult Create()
        {
            return View();
        }
    }
}
