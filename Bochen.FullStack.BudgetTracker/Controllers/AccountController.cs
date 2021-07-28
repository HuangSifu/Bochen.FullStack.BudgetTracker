using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ApplicationCore.Models.Request;
using System.Security.Claims;

namespace Bochen.FullStack.BudgetTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;

        public AccountController(ICurrentUserService currentUserService, IUserService userService,
            IConfiguration config)
        {
            _currentUserService = currentUserService;
            _userService = userService;
            _config = config;
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel registerModel)
        {
            if (!ModelState.IsValid) return View();

            var registeredUser = await _userService.RegisterUser(registerModel);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated) return LocalRedirect("~/");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequestModel loginRequest, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (!ModelState.IsValid) return View();

            var user = await _userService.Login(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return LocalRedirect(returnUrl);
        }
    }
}
