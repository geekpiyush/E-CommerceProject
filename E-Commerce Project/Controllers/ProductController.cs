using Entities.DatabaseContext;
using Entities.ENUM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using RepositoryContracts;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System.Security.Claims;

namespace E_Commerce_Project.Controllers
{
    [Route("[controller]/[action]")]
    
    public class ProductController : Controller
    {
        private readonly IProductDataAddService _dataAddService;
        private readonly IProductDataGetterService _dataGetterService;
        private readonly IProductDataUpdateService _dataUpdateService;
        private readonly IProductDataDeleteService _dataDeleteService;
        private readonly IProductCategoryGetterService _categoryService;
        private readonly IOrderService _orderService;
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public ProductController(ApplicationDbContext db, IProductDataAddService productDataAddService, IProductDataGetterService productDataGetterService, IProductDataUpdateService productDataUpdateService, IProductDataDeleteService productDataDeleteService, IProductCategoryGetterService categoryService, IConfiguration configuration,IOrderService orderService)
        {
            _dataAddService = productDataAddService;
            _dataGetterService = productDataGetterService;
            _dataUpdateService = productDataUpdateService;
            _dataDeleteService = productDataDeleteService;
            _categoryService = categoryService;
            _db = db;
            _configuration = configuration;
            _orderService = orderService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProduct(string searchBy, string? searchString, string sortBy = nameof(ProductDataResponse.ProductName),SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(ProductDataResponse.ProductName),"Product Name" },
                { nameof(ProductDataResponse.ProductID),"ProductID" },
                { nameof(ProductDataResponse.CategoryID),"CategoryID" },
                { nameof(ProductDataResponse.Price),"Price" },
                { nameof(ProductDataResponse.Quantity),"Quantity" }

            };


            List<ProductDataResponse> productData = await _dataGetterService.GetFilterdProduct(searchBy, searchString);

             List<ProductCategoryResponse> categories = await _categoryService.GetAllProductCategories();
            // Map category name to products
            foreach (var product in productData)
            {
                var category = categories.FirstOrDefault(c => c.CategoryID == product.CategoryID);
                if (category != null)
                {
                    product.ProductCategory = category.CategoryName;
                }
            }

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //sortOrder
            List<ProductDataResponse> sortedProduct = _dataGetterService.GetSortedProduct(productData, sortBy, sortOrder);

            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder;

            return View(sortedProduct);
        }
        [AllowAnonymous]
        public async Task<IActionResult> AddProduct()
        {
            List<ProductCategoryResponse> productCategoryResponses = await _categoryService.GetAllProductCategories();

            ViewBag.ProductCategory = productCategoryResponses.Select(temp => new SelectListItem() { Text = temp.CategoryName, Value = temp.CategoryID.ToString()});
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDataAddRequest productDataAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<ProductCategoryResponse> productCategoryResponses = await _categoryService.GetAllProductCategories();

                ViewBag.ProductCategory = productCategoryResponses.Select(temp => new SelectListItem() { Text = temp.CategoryName, Value = temp.CategoryID.ToString() });

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return View();

            }

            ProductDataResponse productData = await _dataAddService.AddProduct(productDataAddRequest);

            return RedirectToAction("GetAllProduct", "Product");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{productID}")]
        public async Task<IActionResult> UpdateProduct(int productID)
        {
            ProductDataResponse productData = await _dataGetterService.GetProductByProductID(productID);

            if (productData == null)
            {
                return RedirectToAction("GetAllProduct", "Product");
            }

            ProductDataUpdateRequest productDataUpdate = productData.ToProductUpdateRequest();

            List<ProductCategoryResponse> productCategoryResponses = await _categoryService.GetAllProductCategories();

            ViewBag.ProductCategory = productCategoryResponses.Select(temp => new SelectListItem() { Text = temp.CategoryName, Value = temp.CategoryID.ToString() });

            return View(productDataUpdate);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{productID}")]
        public async Task<IActionResult> UpdateProduct(ProductDataUpdateRequest productDataUpdate)
        {

           ProductDataResponse? productDataResponse = await _dataGetterService.GetProductByProductID(productDataUpdate.ProductID);

            if(productDataResponse == null)
            {
                return RedirectToAction("GetAllProduct", "Product");
            }

            ValidationHelper.ModelValidation(productDataUpdate);

            ProductDataResponse updatedProduct = await _dataUpdateService.UpdateProductData(productDataUpdate);

            return RedirectToAction("GetAllProduct", "Product");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{productID}")]
        public async Task<IActionResult> DeleteProduct(int productID)
        {
            ProductDataResponse? productDataResponse = await _dataGetterService.GetProductByProductID(productID);

            if(productDataResponse == null)
            {

                return RedirectToAction("GetAllProduct", "Product");
            }

            return View(productDataResponse);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{productID}")]

        public async Task<IActionResult> DeleteProduct(ProductDataUpdateRequest productDataUpdateRequest)
        {
            ProductDataResponse? productDataResponse = await _dataGetterService.GetProductByProductID(productDataUpdateRequest.ProductID);

            if (productDataResponse == null)
            {

                return RedirectToAction("GetAllProduct", "Product");
            }

             await _dataDeleteService.DeleteProductByProductID(productDataUpdateRequest.ProductID);

            return RedirectToAction("GetAllProduct", "Product");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult>BuyNow(int productId)
        {
            // You can fetch product details or any necessary data based on productId
            var product = await _dataGetterService.GetProductByProductID(productId);
            if (product == null)
            {
                return NotFound(); // Or redirect to an error page
            }

            // Pass the product details to the view
            return View(product); 
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CompletePurchase(OrderRequest orderRequest)
        {
            // Fetch product details by ProductID
            var product = await _dataGetterService.GetProductByProductID(orderRequest.ProductID);
            if (product == null)
            {
                return NotFound(); // Handle product not found case
            }

            // Calculate total price
            var totalPrice = product.Price * orderRequest.Quantity;

            // Get Razorpay key and secret from configuration
            var razorpayKey = _configuration["Razorpay:Key"];
            var razorpaySecret = _configuration["Razorpay:Secret"];

            // Step 1: Create a Razorpay Order
            var options = new Dictionary<string, object>();
            options.Add("amount", totalPrice * 100); // Amount in paise (1 INR = 100 paise)
            options.Add("currency", "INR");
            options.Add("receipt", "order_rcptid_11");

            RazorpayClient client = new RazorpayClient(razorpayKey, razorpaySecret);
            Razorpay.Api.Order razorpayOrder = client.Order.Create(options);

            // Prepare response to send back
            var orderResponse = new OrderResponse
            {
                OrderID = razorpayOrder["id"],
                TotalPrice = totalPrice,
                Currency = "INR",
                RazorpayKey = razorpayKey,
                ProductID = orderRequest.ProductID
            };

            // Render PaymentPage to show Razorpay checkout form
            return View("PaymentPage", orderResponse); 
        }

        [Authorize]
        [HttpPost]
        public IActionResult PaymentCallback(IFormCollection form)
        {
            string paymentId = form["razorpay_payment_id"];
            string orderId = form["razorpay_order_id"];
            string signature = form["razorpay_signature"];

            // Retrieve the Razorpay secret from appsettings.json
            var key = _configuration["Razorpay:Secret"];

            // Verify the payment signature with Razorpay
            string expectedSignature = Services.Helpers.Utils.GenerateSignature(orderId, paymentId, key);

            if (expectedSignature.Equals(signature))
            {
                // Payment is successful, store the order in the database
                var order = new Entities.Orders
                {
                    OrderID = orderId,
                    ProductID = Convert.ToInt32(form["product_id"]),
                    FirstName = form["first_name"],
                    LastName = form["last_name"],
                    Address = form["address"],
                    City = form["city"],
                    Country = form["country"],
                    PostalCode = form["postal_code"],
                    Quantity = Convert.ToInt32(form["quantity"]),
                    TotalPrice = Convert.ToDouble(form["total_amount"]),
                    OrderStatus = "Completed",
                    UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))


                };

                // Add order to database
                _db.Orders.Add(order);
                _db.SaveChanges();

                ViewBag.OrderID = order.OrderID; // Ensure you have an OrderID in your order model
                ViewBag.TotalAmount = order.TotalPrice;

                // Redirect to success page
                return View("Success");
            }
            else
            {
                // Payment failed, show failure page
                return View("PaymentFailed");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> PurchaseDetails()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Fetch all orders for this user
            var orders = await _db.Orders
                .Where(o => o.UserId.ToString() == userId)
                .Include(o => o.ProductData) // Include product data if needed
                .ToListAsync();

            // Return an empty list if no orders are found
            return View(orders ?? new List<Entities.Orders>());
        }

        public IActionResult PurchaseDetailsPDF(string orderId)
        {
            var order = _db.Orders.Include(o => o.ProductData) // Include any necessary navigation properties
                .FirstOrDefault(o => o.OrderID == orderId);

            if (order == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("PurchaseDetailsPDF", order, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
            };
        }

    }
}

