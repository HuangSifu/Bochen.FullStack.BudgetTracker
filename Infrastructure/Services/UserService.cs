using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseModel> Login(string email, string password)
        {
            var dbUser = await _userRepository.GetUserByEmail(email);
            if (dbUser == null)
            {
                throw new NotFoundException("Email does not exists, please register first");
            }

            var userPassword = password;

            if (userPassword == dbUser.Password)
            {
                var userLoginRespone = new UserResponseModel
                {

                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    Fullname = dbUser.Fullname,
                    JoinedOn = dbUser.JoinedOn,
                };

                return userLoginRespone;
            }

            return null;
        }

        public async Task<UserResponseModel> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            var delete = await _userRepository.DeleteAsync(user);

            var response = new UserResponseModel()
            {
                Id = delete.Id,
                Fullname = delete.Fullname
            };
            return response;
            //foreach (Income income in user.Incomes)
            //{
            //    await _userRepository.DeleteAsync(income);
            //}
            //foreach (var expenditure in user.Expenditures)
            //{
            //    await _userRepository.DeleteAsync(expenditure);
            //}
            //await _userRepository.DeleteAsync(user);
            //var user = await _userRepository.ListAsync(u => u.Id == id);
            //await _userRepository.DeleteAsync(user.First());
        }

        public async Task<UserResponseModel> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new NotFoundException("User not found!");
            var response = new UserResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                Fullname = user.Fullname,
                JoinedOn = user.JoinedOn,
            };
            return response;
        }


        public async Task<List<UserResponseModel>> ListAllUsers()
        {
            var users = await _userRepository.ListAllAsync();
            var list = new List<UserResponseModel>();
            foreach (var user in users)
            {
                list.Add(new UserResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    JoinedOn = user.JoinedOn,
                });
            }
            return list;
        }

        public async Task<UserResponseModel> RegisterUser(UserRegisterRequestModel registerRequest)
        {
            var dbUser = await _userRepository.GetUserByEmail(registerRequest.Email);
            if (dbUser != null && string.Equals(dbUser.Email, registerRequest.Email, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ConflictException("Email Already exists");
            }
            var user = new User
            {
                Email = registerRequest.Email,
                Password = registerRequest.Password,
                Fullname = registerRequest.Fullname,
                JoinedOn = registerRequest.JoinedOn
            };
            var createdUser = await _userRepository.AddAsync(user);
            var response = new UserResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                Fullname = createdUser.Fullname,
                JoinedOn = createdUser.JoinedOn,
            };
            return response;
        }

        public async Task<UserResponseModel> UpdateUser(UserRegisterRequestModel userUpdateRequest, int id)
        {
            var dbUser = await _userRepository.GetUserByEmail(userUpdateRequest.Email);

            if (dbUser != null && string.Equals(dbUser.Email, userUpdateRequest.Email, StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Email Already Exits");

            var user = new User
            {
                Email = userUpdateRequest.Email,
                Password = userUpdateRequest.Password,
                Fullname = userUpdateRequest.Fullname,
                JoinedOn = userUpdateRequest.JoinedOn
            };

            var updatedUser = await _userRepository.UpdateAsync(user);
            var response = new UserResponseModel
            {
                Id = updatedUser.Id,
                Email = updatedUser.Email,
                Fullname = updatedUser.Fullname,
                JoinedOn = updatedUser.JoinedOn
            };
            return response;
        }

        public async Task<UserDetailsResponseModel> GetUserDetailsById(int id)
        {
            var userDetail = await _userRepository.GetUserById(id);

            var userResponseModel = new UserDetailsResponseModel
            {
                Id = id,
                Fullname = userDetail.Fullname,
                Email = userDetail.Email,
            };

            userResponseModel.Incomes = new List<IncomeResponseModel>();
            foreach (var income in userDetail.Incomes)
            {
                userResponseModel.Incomes.Add(new IncomeResponseModel
                {
                    Id = income.Id,
                    UserId = income.UserId,
                    Amount = income.Amount,
                    Description = income.Description,
                    IncomeDate = income.IncomeDate,
                    Remarks = income.Remarks
                });
            }
            userResponseModel.Expenditures = new List<ExpenditureResponseModel>();
            foreach (var expenditure in userDetail.Expenditures)
            {
                userResponseModel.Expenditures.Add(new ExpenditureResponseModel
                {
                    Id = expenditure.Id,
                    UserId = expenditure.UserId,
                    Amount = expenditure.Amount,
                    Description = expenditure.Description,
                    ExpDate = expenditure.ExpDate,
                    Remarks = expenditure.Remarks
                });
            }

            return userResponseModel;
        }
    }
}
