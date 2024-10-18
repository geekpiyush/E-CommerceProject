using Entities.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Project.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var categories = await _db.ProductCategory.ToListAsync();
            return View(categories);

        }
    }
}

//url: home/index

//url: user/register

//url: user/login
