using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentity_0.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
