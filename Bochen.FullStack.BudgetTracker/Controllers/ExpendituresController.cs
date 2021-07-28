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

        public ExpendituresController(IExpenditureService expenditureService)
        {
            _expenditureService = expenditureService;
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            var addIncome = new IncomeRequestModel
            {
                UserId = id,
            };
            return View(addIncome);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ExpenditureRequestModel model)
        {
            var income = await _expenditureService.AddExpenditure(model);
            return LocalRedirect("~/");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var addIncome = new IncomeRequestModel
            {
                UserId = id,
            };
            return View(addIncome);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ExpenditureRequestModel model)
        {
            var income = await _expenditureService.UpdateExpenditure(model);
            return LocalRedirect("~/");
        }

        
    }
}
