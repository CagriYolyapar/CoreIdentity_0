using CoreIdentity_0.Models.Admins.AppRoles.PageVMs;
using CoreIdentity_0.Models.Admins.AppRoles.ResponseModels;
using CoreIdentity_0.Models.Admins.AppUsers.RequestModels;
using CoreIdentity_0.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreIdentity_0.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            //List<AppUser> allUsers = _userManager.Users.ToList(); 

            List<AppUser> nonAdminUsers = _userManager.Users.Where(x => !x.UserRoles.Any(x => x.Role.Name == "Admin")).ToList();


            return View(nonAdminUsers);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequestModel model)
        {

            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, $"{model.UserName}cgr123");
                if (result.Succeeded)
                {
                    if (await _roleManager.FindByNameAsync("Member") == null) await _roleManager.CreateAsync(new() { Name = "Member" });

                    await _userManager.AddToRoleAsync(appUser, "Member");
                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);


        }

        public async Task<IActionResult> AssignRole(int id)
        {
            AppUser appUser = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == id);

            IList<string> userRoles = await _userManager.GetRolesAsync(appUser); //Elimize gecen kullanıcının rolleri

            List<AppRole> appRoles = _roleManager.Roles.ToList(); //bütün roller

            List<AppRoleResponseModel> roles = new();

            foreach (AppRole role in appRoles)
            {
                roles.Add(new()
                {
                    RoleID = role.Id,
                    RoleName = role.Name,
                    Checked = userRoles.Contains(role.Name)
                });
            }

            AssignRolePageVM arVM = new()
            {
                UserID = id,
                Roles = roles
            };

            return View(arVM);
        }


        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRolePageVM model)
        {
            AppUser appUser = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == model.UserID);
            IList<string> userRoles = await _userManager.GetRolesAsync(appUser);

            foreach (AppRoleResponseModel role in model.Roles)
            {
                if (role.Checked && !userRoles.Contains(role.RoleName)) await _userManager.AddToRoleAsync(appUser, role.RoleName);

                else if (!role.Checked && userRoles.Contains(role.RoleName)) await _userManager.RemoveFromRoleAsync(appUser, role.RoleName);

            }

            return RedirectToAction("Index");
        }
    }
}
