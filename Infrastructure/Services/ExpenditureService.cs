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
    public class ExpenditureService : IExpenditureService
    {
        private readonly IAsyncRepository<Expenditure> _expenditureRepository;

        public ExpenditureService(IAsyncRepository<Expenditure> expenditureRepository)
        {
            _expenditureRepository = expenditureRepository;
        }

        public async Task<ExpenditureResponseModel> AddExpenditure(ExpenditureRequestModel expenditureRequest)
        {
            var expenditure = new Expenditure
            {
                UserId = expenditureRequest.UserId,
                Amount = expenditureRequest.Amount,
                Description = expenditureRequest.Description,
                ExpDate = expenditureRequest.ExpDate,
                Remarks = expenditureRequest.Remarks
            };
            var createdExpenditure = await _expenditureRepository.AddAsync(expenditure);
            var response = new ExpenditureResponseModel
            {
                Id = createdExpenditure.Id,
                UserId = createdExpenditure.UserId,
                Amount = createdExpenditure.Amount,
                Description = createdExpenditure.Description,
                ExpDate = createdExpenditure.ExpDate,
                Remarks = createdExpenditure.Remarks
            };
            return response;
        }

        public async Task<ExpenditureResponseModel> DeleteExpenditure(ExpenditureRequestModel expenditureRequest)
        {
            var expenditure = await _expenditureRepository.GetByIdAsync(expenditureRequest.Id);
            var delete = await _expenditureRepository.DeleteAsync(expenditure);
            var response = new ExpenditureResponseModel
            {
                Amount = delete.Amount,
                Description = delete.Description,
                ExpDate = delete.ExpDate,
                Remarks = delete.Remarks
            };
            return response;
        }


        public async Task<ExpenditureResponseModel> UpdateExpenditure(ExpenditureRequestModel expenditureRequest)
        {
            var expenditure = new Expenditure
            {
                UserId = expenditureRequest.UserId,
                Amount = expenditureRequest.Amount,
                Description = expenditureRequest.Description,
                ExpDate = expenditureRequest.ExpDate,
                Remarks = expenditureRequest.Remarks,
            };
            var updatedExpenditure = await _expenditureRepository.UpdateAsync(expenditure);
            var response = new ExpenditureResponseModel
            {
                Id = updatedExpenditure.Id,
                UserId = updatedExpenditure.UserId,
                Amount = updatedExpenditure.Amount,
                Description = updatedExpenditure.Description,
                ExpDate = updatedExpenditure.ExpDate,
                Remarks = updatedExpenditure.Remarks,
            };
            return response;
        }


        public async Task<IEnumerable<ExpenditureResponseModel>> ListAllExpendituresByUser(int id)
        {
            var expenditures = await _expenditureRepository.ListAsync(e => e.UserId == id);
            var response = new List<ExpenditureResponseModel>();
            foreach (var expenditure in expenditures)
            {
                response.Add(new ExpenditureResponseModel
                {
                    Id = expenditure.Id,
                    UserId = expenditure.UserId,
                    Amount = expenditure.Amount,
                    Description = expenditure.Description,
                    ExpDate = expenditure.ExpDate,
                    Remarks = expenditure.Remarks,
                });
            }
            return response;
        }

        public async Task<IEnumerable<ExpenditureResponseModel>> ListAllExpenditures()
        {
            var expenditures = await _expenditureRepository.ListAllAsync();
            var response = new List<ExpenditureResponseModel>();
            foreach (var expenditure in expenditures)
            {
                response.Add(new ExpenditureResponseModel
                {
                    Id = expenditure.Id,
                    UserId = expenditure.UserId,
                    Amount = expenditure.Amount,
                    Description = expenditure.Description,
                    ExpDate = expenditure.ExpDate,
                    Remarks = expenditure.Remarks,
                });
            }
            return response;
        }

    }
}
