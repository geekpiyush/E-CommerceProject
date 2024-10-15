using Entities.DatabaseContext;
using Entities.ENUM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

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
        private readonly IProductCategoryGetterService _categoryService;
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db, IProductDataAddService productDataAddService, IProductDataGetterService productDataGetterService, IProductDataUpdateService productDataUpdateService, IProductDataDeleteService productDataDeleteService, IProductCategoryGetterService categoryService)
        {
            _dataAddService = productDataAddService;
            _dataGetterService = productDataGetterService;
            _dataUpdateService = productDataUpdateService;
            _dataDeleteService = productDataDeleteService;
            _categoryService = categoryService;
            _db = db;

        }

        public IActionResult GetAllProduct(string searchBy, string? searchString, string sortBy = nameof(ProductDataResponse.ProductName),SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(ProductDataResponse.ProductName),"Product Name" },
                { nameof(ProductDataResponse.ProductID),"ProductID" },
                { nameof(ProductDataResponse.CategoryID),"CategoryID" },
                { nameof(ProductDataResponse.Price),"Price" },
                { nameof(ProductDataResponse.Quantity),"Quantity" }

            };
            List<ProductDataResponse> productData = _dataGetterService.GetFilterdProduct(searchBy, searchString);

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //sortOrder
            List<ProductDataResponse> sortedProduct = _dataGetterService.GetSortedProduct(productData, sortBy, sortOrder);

            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder;

            return View(sortedProduct);
        }

        public IActionResult AddProduct()
        {
            List<ProductCategoryResponse> productCategoryResponses = _categoryService.GetAllProductCategories();

            ViewBag.ProductCategory = productCategoryResponses;
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductDataAddRequest productDataAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<ProductCategoryResponse> productCategoryResponses = _categoryService.GetAllProductCategories();

                ViewBag.ProductCategory = productCategoryResponses;

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

            }

            ProductDataResponse productData = _dataAddService.AddProduct(productDataAddRequest);

            return RedirectToAction("GetAllProduct", "Product");
        }

        [HttpGet]
        [Route("{productID}")]
        public IActionResult UpdateProduct(int productID)
        {
            ProductDataResponse productData = _dataGetterService.GetProductByProductID(productID);

            if (productData == null)
            {
                return RedirectToAction("GetAllProduct", "Product");
            }

            ProductDataUpdateRequest productDataUpdate = productData.ToProductUpdateRequest();

            List<ProductCategoryResponse> productCategoryResponses = _categoryService.GetAllProductCategories();

            ViewBag.ProductCategory = productCategoryResponses.Select(temp => new SelectListItem() { Text = temp.CategoryName, Value = temp.CategoryID.ToString() });

            return View(productDataUpdate);
        }

        [HttpPost]
        [Route("{productID}")]
        public IActionResult UpdateProduct(ProductDataUpdateRequest productDataUpdate)
        {

           ProductDataResponse productDataResponse =  _dataGetterService.GetProductByProductID(productDataUpdate.ProductID);

            if(productDataResponse == null)
            {
                return RedirectToAction("GetAllProduct", "Product");
            }

            ValidationHelper.ModelValidation(productDataUpdate);

            ProductDataResponse updatedProduct = _dataUpdateService.UpdateProductData(productDataUpdate);

            return RedirectToAction("GetAllProduct", "Product");
        }

        [HttpGet]
        [Route("{productID}")]
        public IActionResult DeleteProduct(int productID)
        {
            ProductDataResponse? productDataResponse = _dataGetterService.GetProductByProductID(productID);

            if(productDataResponse == null)
            {

                return RedirectToAction("GetAllProduct", "Product");
            }

            return View(productDataResponse);
        }


        [HttpPost]
        [Route("{productID}")]

        public IActionResult DeleteProduct(ProductDataUpdateRequest productDataUpdateRequest)
        {
            ProductDataResponse? productDataResponse = _dataGetterService.GetProductByProductID(productDataUpdateRequest.ProductID);

            if (productDataResponse == null)
            {

                return RedirectToAction("GetAllProduct", "Product");
            }

             _dataDeleteService.DeleteProductByProductID(productDataUpdateRequest.ProductID);

            return RedirectToAction("GetAllProduct", "Product");
        }
    }
}
