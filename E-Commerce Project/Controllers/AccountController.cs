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
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

         return View(); 

        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO,string? ReturnUrl)
        {
            // check validation error
            if(ModelState.IsValid == false)
            {

                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);
                return View(loginDTO);
            }

              var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password,isPersistent:false,lockoutOnFailure:false);

            if(result.Succeeded)
            {

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ModelState.AddModelError("Login", "Invalid Email or Password");

            return View();

        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            if (ModelState.IsValid == false)
            {

                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);
                return View(forgetPasswordDTO);
            }

            var user = await _userManager.FindByEmailAsync(forgetPasswordDTO.Email);

            if(user == null)
            {
                ModelState.AddModelError("ForgetPassword","User with this email does not exist");
                return View(forgetPasswordDTO);
            }

            var IsOldPasswordValid = await _userManager.CheckPasswordAsync(user, forgetPasswordDTO.OldPassword);

            if (IsOldPasswordValid == false)
            {
                ModelState.AddModelError("ForgetPassword", "Old Password is incorrect");

                return View(forgetPasswordDTO);
            }

            var result = await _userManager.ChangePasswordAsync(user,forgetPasswordDTO.OldPassword, forgetPasswordDTO.NewPassword);

            if(result.Succeeded)
            {
                //Password Change Successful
                return RedirectToAction(nameof(HomeController.Index), "Home");

            }

            return View(forgetPasswordDTO);
        }

        //Logout
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }


    }
}
