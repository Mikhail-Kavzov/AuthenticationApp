using AuthenticationApp.Models;
using AuthenticationApp.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return RedirectToAction("Authenticate");
        }

        [HttpGet]
        public IActionResult Register() => View();

        public IActionResult Authenticate() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authenticate(AuthenticateViewModel autForm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(autForm.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "User with current Email not found");
                    return View(autForm);
                }
                if (user.Status == UserStatus.Blocked)
                {
                    ModelState.AddModelError(string.Empty, "User has been blocked");
                    return View(autForm);
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, autForm.Password, false, false);
                if (result.Succeeded)
                {
                    user.LastLogin = DateTime.Now;
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Password", "Incorrect Password");

            }
            return View(autForm);
        }

        [NonAction]
        private static User CreateNewUser(RegisterViewModel regForm)
        {
            DateTime timeNow = DateTime.Now;
            User newUser = new()
            {
                Email = regForm.Email,
                UserName = regForm.Name,
                LastLogin = timeNow,
                RegistryData = timeNow,
                Status = UserStatus.Active
            };
            return newUser;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regForm)
        {
            if (ModelState.IsValid)
            {
                User user = CreateNewUser(regForm);
                var result = await _userManager.CreateAsync(user, regForm.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(regForm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Authenticate");
        }
    }
}
