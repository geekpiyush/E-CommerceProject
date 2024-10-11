using Entities.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace E_Commerce_Project.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly IProductDataAddService _dataAddService;
        private readonly IProductDataGetterService _dataGetterService;
        private readonly IProductDataUpdateService _dataUpdateService;
        private readonly IProductDataDeleteService _dataDeleteService;
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db, IProductDataAddService productDataAddService, IProductDataGetterService productDataGetterService, IProductDataUpdateService productDataUpdateService, IProductDataDeleteService productDataDeleteService)
        {
            _dataAddService = productDataAddService;
            _dataGetterService = productDataGetterService;
            _dataUpdateService = productDataUpdateService;
            _dataDeleteService = productDataDeleteService;
            _db = db;
        }

        public IActionResult GetAllProduct(string searchBy, string? searchString)
        {

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(ProductDataResponse.ProductName),"Product Name" },
                { nameof(ProductDataResponse.ProductID),"ProductID" },
                { nameof(ProductDataResponse.CategoryID),"CategoryID" },
                { nameof(ProductDataResponse.Price),"Price" },
                { nameof(ProductDataResponse.Quantity),"Quantity" }

            };
            List<ProductDataResponse> productData = _dataGetterService.GetFilterdProduct(searchBy,searchString);

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            return View(productData);
        }
    }
}
