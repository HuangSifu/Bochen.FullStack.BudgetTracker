using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IExpenditureService
    {
        Task<ExpenditureResponseModel> AddExpenditure(ExpenditureRequestModel expenditureRequest);
        Task<ExpenditureResponseModel> DeleteExpenditure(int id);
        Task<ExpenditureResponseModel> UpdateExpenditure(ExpenditureRequestModel expendituRequest);
        Task<IEnumerable<ExpenditureResponseModel>> ListAllExpendituresByUser(int id);
        Task<IEnumerable<ExpenditureResponseModel>> ListAllExpenditures();
    }
}
