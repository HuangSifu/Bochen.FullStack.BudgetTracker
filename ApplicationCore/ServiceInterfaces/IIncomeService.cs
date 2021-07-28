using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IIncomeService
    {
        Task<IncomeResponseModel> AddIncome(IncomeRequestModel incomeRequest);
        Task<IncomeResponseModel> UpdateIncome(IncomeRequestModel incomeRequest);
        Task<IncomeResponseModel> DeleteIncome(IncomeRequestModel incomeRequestModel);
        Task<IncomeResponseModel> GetIncomeById(int id);
        Task<IEnumerable<IncomeResponseModel>> ListAllIncomesByUser(int id);
        Task<IEnumerable<IncomeResponseModel>> ListAllIncomes();
    }
}
