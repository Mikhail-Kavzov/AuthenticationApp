using AuthenticationApp.Hash;
using AuthenticationApp.Models;
using AuthenticationApp.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;

        public AccountController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpGet]
        public IActionResult Authenticate() => View();

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticateViewModel autForm)
        {
            if (ModelState.IsValid)
            {
                User? user = await userRepository.GetUserByEmail(autForm.Email);
                if (user == null)
                    return View(); //user not exist
                if (user.Status == UserStatus.Blocked) //user is blocked
                    return View();
                string autPasswordHash = HashPassword.GenerateHash(autForm.Password);
                if (autPasswordHash != user.Password) //password are not equal
                    return View();
                user.LastLogin=DateTime.Now;
                userRepository.Update(user);
                await userRepository.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [NonAction]
        private void CreateNewUser(RegisterViewModel regForm)
        {
            string passwordHash = HashPassword.GenerateHash(regForm.Password);
            DateTime timeNow = DateTime.Now;
            User newUser = new()
            {
                UserEmail = regForm.Email,
                UserName = regForm.Name,
                Password = passwordHash,
                LastLogin = timeNow,
                RegistryData = timeNow,
                Status = UserStatus.Active
            };
            userRepository.Create(newUser);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regForm)
        {
            if (ModelState.IsValid)
            {
                User? userExist = await userRepository.GetUserByEmail(regForm.Email);
                if (userExist != null) //if user exists
                    return View();
                CreateNewUser(regForm);
                await userRepository.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
