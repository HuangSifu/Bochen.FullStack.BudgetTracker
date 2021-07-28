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
        
        private readonly IExpenditureService _expenditureService;
        public UsersController(IUserService userService, IExpenditureService expenditureService)
        {
            _userService = userService;
            _expenditureService = expenditureService;
        }
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserDetailsById(id);
            return View(user);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deletUser = await _userService.DeleteUser(id);
            return LocalRedirect("~/");
        }
    }
}
