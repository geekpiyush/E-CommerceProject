
using Entities.ENUM;
using Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;

namespace E_Commerce_Project.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationUserRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationUserRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            //Check Validation Error

            if(ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);
                return View(registerDTO);
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                CustomerName = registerDTO.CustomerName,
                UserName = registerDTO.Email,
                PhoneNumber = registerDTO.Phone,

            };

              IdentityResult result =  await _userManager.CreateAsync(user,registerDTO.Password);

            if(result.Succeeded)
            {
                if(registerDTO.UserType == UserTypeOptions.Admin)
                {
                    //create admin role
                    if(await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) == null)
                    {
                        ApplicationUserRole applicationUserRole = new ApplicationUserRole() { Name = UserTypeOptions.Admin.ToString() };
                       await _roleManager.CreateAsync(applicationUserRole);
                    }

                    //Add user to Admin Role
                   await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
                }
                else
                {
                    //Create customer role
                    ApplicationUserRole applicationUserRole = new ApplicationUserRole() { Name = UserTypeOptions.Customer.ToString() };
                    await _roleManager.CreateAsync(applicationUserRole);
                    //Add user to Customer Role
                    await _userManager.AddToRoleAsync(user, UserTypeOptions.Customer.ToString());
                }

                //Sign in user
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }

                return View(registerDTO);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {

            return PartialView("_loginPartialView");

        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string? ReturnUrl)
        {
            // Check validation errors
            if (!ModelState.IsValid)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return PartialView("_LoginPartialView", loginDTO);
                }

                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);
                return View(loginDTO);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true });
                }

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ModelState.AddModelError("Login", "Invalid Email or Password");

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_LoginPartialView", loginDTO);
            }

            return View(loginDTO);
        }


        //Logout
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }
    }
}
