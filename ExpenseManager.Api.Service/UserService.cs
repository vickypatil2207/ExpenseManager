using ExpenseManager.Api.Database.Entities;
using ExpenseManager.Api.Repository.Interfaces;
using ExpenseManager.Api.Service.Interfaces;
using ExpenseManager.Shared.Models;
using ExpenseManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Api.Service.Mapper;
using ExpenseManager.Shared.Helpers;

namespace ExpenseManager.Api.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepositry;
        public UserService(IRepository<User> userRepository)
        {
            _userRepositry = userRepository;
        }

        public async Task<ServiceResult<UserModel>> CreateUser(UserModel userModel)
        {
            var user = UserMapper.MapToUserEntity(userModel);
            user.Password = HashHelper.ComputeHash(userModel.Password);

            var result = await _userRepositry.CreateAsync(user);
            if (result.Id > 0)
            {
                var userCreated = await _userRepositry.GetByIdAsync(result.Id);
                if (userCreated != null)
                {
                    return ServiceResult<UserModel>.Ok(UserMapper.MapToUserModel(userCreated));
                }
            }

            return ServiceResult<UserModel>.Fail("Something went wrong saving user details!", userModel);
        }

        public async Task<ServiceResult<UserModel>> UpdateUser(int id, UserModel userModel)
        {
            var userResult = _userRepositry.FirstOrDefault(u => u.Id == id && u.IsActive);
            if (userResult != null)
            {
                UserMapper.MapToUserEntity(userModel, ref userResult);
                await _userRepositry.UpdateAsync(userResult);

                var userUpdated = await _userRepositry.GetByIdAsync(id);
                if (userUpdated != null)
                {
                    return ServiceResult<UserModel>.Ok(UserMapper.MapToUserModel(userUpdated));
                }

                return ServiceResult<UserModel>.Fail($"Something went wrong saving user details for Id = {id}!", userModel);
            }

            return ServiceResult<UserModel>.Fail($"Could not find user details for Id = {id}!", userModel);
        }

        public async Task<ServiceResult<UserModel>> DeactivateUser(int id)
        {
            var userResult = _userRepositry.FirstOrDefault(u => u.Id == id && u.IsActive);
            if (userResult != null)
            {
                userResult.IsActive = false;
                await _userRepositry.UpdateAsync(userResult);

                var userUpdated = await _userRepositry.GetByIdAsync(id);
                if (userUpdated != null)
                {
                    return ServiceResult<UserModel>.Ok(UserMapper.MapToUserModel(userUpdated));
                }

                return ServiceResult<UserModel>.Fail($"Something went wrong deleting user details for Id = {id}!", UserMapper.MapToUserModel(userResult));
            }

            return ServiceResult<UserModel>.Fail($"Could not find user details for Id = {id}!");
        }

        public async Task<ServiceResult<UserModel>> GetUserById(int id)
        {
            var userResult = _userRepositry.FirstOrDefault(u => u.Id == id && u.IsActive);
            if (userResult != null)
            {
                return ServiceResult<UserModel>.Ok(UserMapper.MapToUserModel(userResult));
            }

            return ServiceResult<UserModel>.Fail($"Could not find user details for Id = {id}!");
        }

        public async Task<ServiceResult<UserModel>> SignIn(SigninModel signinModel)
        {
            var searchResult = _userRepositry.Search(u => (u.Username.ToLower() == signinModel.Username.ToLower() 
                            || u.Email.ToLower() == signinModel.Username.ToLower())
                        && HashHelper.VerifyHash(signinModel.Password, u.Password)
                        && u.IsActive);
            if (searchResult != null && searchResult.Any())
            {
                var user = searchResult.First();
                return ServiceResult<UserModel>.Ok(UserMapper.MapToUserModel(user));
            }

            return ServiceResult<UserModel>.Fail($"Could not find user details for Username = {signinModel.Username} & Password = {signinModel.Password}!");
        }
    }
}
