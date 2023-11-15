using CoreIdentity_0.Models;
using CoreIdentity_0.Models.AppUsers.RequestModels;
using CoreIdentity_0.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult; //Namespace'inizde özel Tip ismi tanımlama

namespace CoreIdentity_0.Controllers
{
    [AutoValidateAntiforgeryToken] //Get ile gelen sayfada verilen ozel bir token sayesinde Post'un bu tokensiz yapılamamasını saglar..PostMan gibi third party software'lerden gözlemlediginizde direkt Post tarafına ulasamadıgınızı göreceksiniz...
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

                //AppUserProfile profile = new();
                //profile.ID = appUser.Id;

                if (result.Succeeded)
                {
                    #region AdminEklemeIcinTekKullanimlikKodlar
                    //AppRole appRole = await _roleManager.FindByNameAsync("Admin"); //Admin ismindeki rolu bulabilirse Role nesnesini appRole'e atacak bulamazsa appRole null olacak
                    //if (appRole == null) await _roleManager.CreateAsync(new() { Name = "Admin" }); //Admin isminde bir rol yarattık
                    //await _userManager.AddToRoleAsync(appUser, "Admin"); //appUser degişkeninin tuttuğu kullanıcı nesnesini Admin isimli Role'e ekledik 


                    #endregion

                    await _userManager.AddToRoleAsync(appUser, "Member"); //Register artık bu kod sayesinde direkt Member rolüne sahip olacaktır

                    return RedirectToAction("Index");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(model);
        }


        //Unutmayın ki Identity User nesnesine ulasabilmek icin Authorize attribute'unuzun olması gereklidir


        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        [Authorize(Roles = "Member")]
        public IActionResult MemberPanel()
        {
           
            return View();
        }

        public IActionResult Panel()
        {
            return View();
        }

        public IActionResult SignIn(string returnUrl)
        {
            UserSignInRequestModel usModel = new()
            {
                ReturnUrl = returnUrl
            };

            return View(usModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInRequestModel model)
        {
            if (ModelState.IsValid)
            {
               
                AppUser appUser = await _userManager.FindByNameAsync(model.UserName); //await ile bir Task'in direkt sonucunu beklediginiz icin onu elde edersiniz...

                SignInResult result = await _signInManager.PasswordSignInAsync(appUser, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    IList<string> roles = await _userManager.GetRolesAsync(appUser);
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("AdminPanel");
                    }
                    else if (roles.Contains("Member"))
                    {
                        return RedirectToAction("Member");
                    }

                    return RedirectToAction("Panel");

                }

                else if (result.IsLockedOut)
                {
                   
                   
                    DateTimeOffset? lockOutEndDate = await _userManager.GetLockoutEndDateAsync(appUser);

                    ModelState.AddModelError("", $"Hesabınız {(lockOutEndDate.Value.UtcDateTime -DateTime.UtcNow).Minutes} dakika süreyle askıya alınmıstır");
                }

                else
                {
                    string message = "";

                    if (appUser != null)
                    {
                        int maxFailedAttempts = _userManager.Options.Lockout.MaxFailedAccessAttempts;// middlewaredaki maksimum hata sayınız ..._userManager'daki ise su ana kadar kac kez yanlıslık yaptıgınız
                        message = $"Eger {maxFailedAttempts - await _userManager.GetAccessFailedCountAsync(appUser)} kez daha yanlıs giriş yaparsanız hesabınız gecici olarak askıya alınacaktır " ;
                    }
                    else
                    {
                        message = "Kullanıcı bulunamadı";
                    }

                    ModelState.AddModelError("", message);
                }




            }
            return View(model);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}