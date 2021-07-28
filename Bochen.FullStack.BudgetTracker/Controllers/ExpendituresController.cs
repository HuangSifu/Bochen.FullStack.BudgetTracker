using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bochen.FullStack.BudgetTracker.Controllers
{
    public class ExpendituresController : Controller
    {
        private readonly IExpenditureService _expenditureService;
        private readonly ICurrentUserService _currentUserService;

        public ExpendituresController(IExpenditureService expenditureService, ICurrentUserService currentUserService)
        {
            _expenditureService = expenditureService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            if(_currentUserService.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ExpenditureRequestModel model)
        {
            var income = await _expenditureService.AddExpenditure(model);
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
        public async Task<IActionResult> Update(ExpenditureRequestModel model)
        {
            var income = await _expenditureService.UpdateExpenditure(model);
            return LocalRedirect("~/");
        }

        
    }
}
