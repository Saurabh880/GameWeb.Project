using GameManager.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GameFrontEnd.Mvc.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDTO registerUser)
        {
            //Register User and Save Data to Identity DB
            return RedirectToAction(nameof(GameController.Index), "Game");
        }
    }
}
