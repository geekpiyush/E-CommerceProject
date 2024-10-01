using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Project.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();

        }
    }
}

//url: home/index

//url: user/register

//url: user/login
