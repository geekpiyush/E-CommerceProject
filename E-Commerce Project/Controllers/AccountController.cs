using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Project.Controllers
{
    public class AccountController : Controller
    {
        [Route("account/register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
