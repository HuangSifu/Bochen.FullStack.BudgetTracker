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

        public IncomesController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
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
        public async Task<IActionResult> Add(IncomeRequestModel model)
        {
            var income = await _incomeService.AddIncome(model);
            return LocalRedirect("~/");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var income = await _incomeService.GetIncomeById(id);

            var addIncome = new IncomeRequestModel
            {
                UserId = income.UserId,
                Amount = (decimal)income.Amount,
                Description = income.Description,
                IncomeDate = income.IncomeDate.GetValueOrDefault(),
                Remarks = income.Remarks,
            };
            return View(addIncome);
        }
        [HttpPost]
        public async Task<IActionResult> Update(IncomeRequestModel model)
        {
            var income = await _incomeService.UpdateIncome(model);
            return LocalRedirect("~/");
        }
       
    }
}
