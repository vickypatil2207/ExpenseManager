using ExpenseManager.Shared;
using ExpenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Service.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<UserModel>> CreateUser(UserModel userModel);
        Task<ServiceResult<UserModel>> UpdateUser(int id, UserModel userModel);
        Task<ServiceResult<UserModel>> DeactivateUser(int id);
        Task<ServiceResult<UserModel>> GetUserById(int id);
        Task<ServiceResult<UserModel>> SignIn(SigninModel signinModel);
    }
}
