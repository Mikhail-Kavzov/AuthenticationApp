using AuthenticationApp.Models;
using AuthenticationApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuthenticationApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public HomeController(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }

        [NonAction]
        private void ChangeUserStatus(IEnumerable<User> users, UserStatus status)
        {
            foreach (var user in users)
            {
                user.Status = status;
                _userRepository.Update(user);
            }
        }

        [NonAction]
        private async Task<bool> CheckCurrentStatusUser()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return currentUser == null || currentUser?.Status == UserStatus.Blocked;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsers(string[] id)
        {
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            var users = await _userRepository.GetUsersAsync(id);
            foreach (var user in users)
            {
                _userRepository.Delete(user);
            }
            await _userRepository.SaveChangesAsync();
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            return Json("");
        }

        [HttpPut]
        public async Task<IActionResult> UnBlockUsers(string[] id)
        {
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            var users = await _userRepository.GetUsersAsync(id);
            ChangeUserStatus(users, UserStatus.Active);
            await _userRepository.SaveChangesAsync();
            return Json("");
        }

        [HttpPut]
        public async Task<IActionResult> BlockUsers(string[] id)
        {
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            var users = await _userRepository.GetUsersAsync(id);
            ChangeUserStatus(users, UserStatus.Blocked);
            await _userRepository.SaveChangesAsync();
            if (await CheckCurrentStatusUser())
                return Json(new { redirectToUrl = Url.Action("Logout", "Account") });
            return Json("");
        }
    }
}