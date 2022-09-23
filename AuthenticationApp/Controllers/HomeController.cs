using AuthenticationApp.Models;
using AuthenticationApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuthenticationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;

        public HomeController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;       
        }

        public async Task<IActionResult> Index()
        {
            var users=await userRepository.GetAllAsync();
            return View(users);
        }
    }
}