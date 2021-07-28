using ApplicationCore.Entities;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IAsyncRepository<Income> _incomeRepository;

        public IncomeService(IAsyncRepository<Income> incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task<IncomeResponseModel> AddIncome(IncomeRequestModel incomeRequest)
        {
            var income = new Income
            {
                UserId = incomeRequest.UserId,
                Amount = incomeRequest.Amount,
                Description = incomeRequest.Description,
                IncomeDate = incomeRequest.IncomeDate,
                Remarks = incomeRequest.Remarks,
            };

            var createdIncome = await _incomeRepository.AddAsync(income);
            var response = new IncomeResponseModel
            {
                Id = createdIncome.Id,
                UserId = createdIncome.UserId,
                Amount = createdIncome.Amount,
                Description = createdIncome.Description,
                IncomeDate = createdIncome.IncomeDate,
                Remarks = createdIncome.Remarks
            };
            return response;
        }


        public async Task<IEnumerable<IncomeResponseModel>> ListAllIncomes()
        {
            var incomes = await _incomeRepository.ListAllAsync();

            var response = new List<IncomeResponseModel>();
            foreach (var income in incomes)
            {
                response.Add(new IncomeResponseModel
                {
                    Id = income.Id,
                    UserId = income.Id,
                    Amount = income.Amount,
                    Description = income.Description,
                    IncomeDate = income.IncomeDate,
                    Remarks = income.Remarks
                });
            }
            return response;
        }

        public async Task<IEnumerable<IncomeResponseModel>> ListAllIncomesByUser(int id)
        {
            var incomes = await _incomeRepository.ListAsync(e => e.UserId == id);
            var response = new List<IncomeResponseModel>();
            foreach (var income in incomes)
            {
                response.Add(new IncomeResponseModel
                {
                    Id = income.Id,
                    UserId = income.UserId,
                    Amount = (decimal)income.Amount,
                    Description = income.Description,
                    IncomeDate = income.IncomeDate,
                    Remarks = income.Remarks,
                });
            }
            return response;
        }

        public async Task<IncomeResponseModel> UpdateIncome(IncomeRequestModel incomeRequest)
        {
            var income = new Income
            {
                Id = incomeRequest.Id,
                UserId = incomeRequest.UserId,
                Amount = incomeRequest.Amount,
                Description = incomeRequest.Description,
                IncomeDate = incomeRequest.IncomeDate,
                Remarks = incomeRequest.Remarks
            };
            var updatedIncome = await _incomeRepository.UpdateAsync(income);
            var response = new IncomeResponseModel
            {
                Amount = updatedIncome.Amount,
                Description = updatedIncome.Description,
                IncomeDate = updatedIncome.IncomeDate,
                Remarks = updatedIncome.Remarks
            };
            return response;
        }
        public async Task<IncomeResponseModel> DeleteIncome(IncomeRequestModel incomeRequestModel)
        {
            var incomes = await _incomeRepository.GetByIdAsync(incomeRequestModel.Id);
            var delete = await _incomeRepository.DeleteAsync(incomes);
            var response = new IncomeResponseModel
            {
                Amount = delete.Amount,
                Description = delete.Description,
                IncomeDate = delete.IncomeDate,
                Remarks = delete.Remarks
            };
            return response;
        }

        public async Task<IncomeResponseModel> GetIncomeById(int id)
        {
            var income = await _incomeRepository.GetByIdAsync(id);
            var response = new IncomeResponseModel
            {
                Id = income.Id,
                Amount = income.Amount,
                Description = income.Description,
                IncomeDate = income.IncomeDate,
                Remarks = income.Remarks,
            };
            return response;
        }
    }
}
