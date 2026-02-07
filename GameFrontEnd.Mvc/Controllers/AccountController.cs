using GameManager.Core.Domain.IdentityEntities;
using GameManager.Core.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameFrontEnd.Mvc.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager; //Sign in Manager with Data type/Class
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
        public async Task<IActionResult> Register(RegisterDTO registerUser)
        {
            //Check for validation error
            //Lecture 336 - User Manager
            if(ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(registerUser);
            }
            ApplicationUser newUser = new ApplicationUser()
            {
                Email = registerUser.Email,
                PhoneNumber = registerUser.Phone,
                UserName = registerUser.Email,
                PersonName = registerUser.PersonName
            };
            //Register User and Save Data to Identity DB
            IdentityResult result =  await _userManager.CreateAsync(newUser, registerUser.Password);

            if (result.Succeeded)
            {
                //Sign In - to log in user
                await _signInManager.SignInAsync(newUser,isPersistent: false);

                return RedirectToAction(nameof(GameController.Index),"Game");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Resgister ",error.Description);
                }
            }

            return View(registerUser);
        }
    }
}
