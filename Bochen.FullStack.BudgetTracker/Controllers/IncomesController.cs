using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bochen.FullStack.BudgetTracker.Controllers
{
    public class IncomesController : Controller
    {
        private readonly IIncomeService _incomeService;
        private readonly ICurrentUserService _currentUserService;

        public IncomesController(IIncomeService incomeService, ICurrentUserService currentUserService)
        {
            _incomeService = incomeService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (_currentUserService.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(IncomeRequestModel model)
        {
            var income = await _incomeService.AddIncome(model);
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
        public async Task<IActionResult> Update(IncomeRequestModel model)
        {
            await _incomeService.UpdateIncome(model);
            return LocalRedirect("~/");
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
       public async Task<IActionResult> Delete(int id)
        {
            var deleteIncome = new IncomeRequestModel()
            {
                Id = id
            };
            await _incomeService.DeleteIncome(deleteIncome);
            return LocalRedirect("~/");
        }
    }
}
