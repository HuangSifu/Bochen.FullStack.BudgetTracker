using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserResponseModel> Login(string email, string password);
        Task<UserResponseModel> RegisterUser(UserRegisterRequestModel registerRequest);
        Task<UserResponseModel> GetUserById(int id);
        Task<UserResponseModel> UpdateUser(UserRegisterRequestModel userUpdateRequest, int id);
        Task<UserResponseModel> DeleteUser(int id);
        Task<List<UserResponseModel>> ListAllUsers();
        Task<UserDetailsResponseModel> GetUserDetailsById(int id);
    }
}
