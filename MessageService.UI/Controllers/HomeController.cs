using MessageService.UI.Models;
using MessageService.UI.Repos.IRepos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepo _userRepo;
        public HomeController(ILogger<HomeController> logger, IUserRepo userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            HttpContext.Request.Cookies.TryGetValue("isLogIn", out string value);
            if (string.IsNullOrEmpty(value))
            {
                return RedirectToAction(nameof(Login));
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var userData = await _userRepo.Get(a => a.Mail == user.Mail);

            if(userData != null && userData.Pass == user.Pass)
            {
                CookieOptions cookie = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                };
                Response.Cookies.Append("isLogIn", "true", cookie);
                Response.Cookies.Append("userName", userData.Name, cookie);
                Response.Cookies.Append("userId", userData.Id.ToString(), cookie);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
