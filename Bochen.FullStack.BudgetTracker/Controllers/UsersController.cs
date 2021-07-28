using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bochen.FullStack.BudgetTracker.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public UsersController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserDetailsById(id);
            return View(user);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            if (_currentUserService.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserUpdateRequestModel model)
        {
            await _userService.DeleteUser(model);
            return LocalRedirect("~/");
        }
        [HttpGet]
        public IActionResult Update()
        {
            if (_currentUserService.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateRequestModel model)
        {
            await _userService.UpdateUser(model);
            return LocalRedirect("~/");
        }
    }
}
