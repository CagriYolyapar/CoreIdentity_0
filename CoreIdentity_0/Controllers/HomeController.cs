using CoreIdentity_0.Models;
using CoreIdentity_0.Models.AppUsers.RequestModels;
using CoreIdentity_0.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreIdentity_0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //Identity kütüphanesi crud ve servis islemleri icin bir takım class'lara sahiptir...Bu Manager class'ları sizin ilgili Identity yapılarınızın Crud işlemlerine ve baska business logic işlemlerine girmesini saglarlar...

        //Constructor hell durumunu arastırın...

        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;
        readonly SignInManager<AppUser> _signInManager;



        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    UserName = model.UserName,
                    Email = model.Email



                };

                IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);

               //Todo: İşlemler burada kaldı. IdentityResult üzerinden devam edilecek
               //Serra : Buradan sonra Admin işlemleri de yapılacak unutma
            }
        }
    }
}