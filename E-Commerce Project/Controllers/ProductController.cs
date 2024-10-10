using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Project.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        public IActionResult GetAllProduct()
        {
            return View();
        }
    }
}
