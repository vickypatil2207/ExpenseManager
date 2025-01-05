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
        private readonly IExpenseCategoryService _expenseCategoryService;
        private readonly ITokenService _tokenService;

        public UserService(IRepository<User> userRepository, 
                IExpenseCategoryService expenseCategoryService,
                ITokenService tokenService)
        {
            _userRepositry = userRepository;
            _expenseCategoryService = expenseCategoryService;
            _tokenService = tokenService;
        }

        public async Task<ServiceResult<TokenModel>> CreateUser(UserModel userModel)
        {
            var usernameAlreadyPresent = await _userRepositry.FirstOrDefaultAsync(u => u.Username.ToLower() == userModel.Username.ToLower());
            if (usernameAlreadyPresent != null)
            {
                return ServiceResult<TokenModel>.Fail($"User Already Present for Username = {userModel.Username}!", UserMapper.MapToTokenModel(usernameAlreadyPresent));
            }

            var emailAlreadyPresnet = await _userRepositry.FirstOrDefaultAsync(u => u.Email.ToLower() == userModel.Email.ToLower());
            if (emailAlreadyPresnet != null)
            {
                return ServiceResult<TokenModel>.Fail($"User Already Present for Email = {userModel.Email}!", UserMapper.MapToTokenModel(emailAlreadyPresnet));
            }

            var user = UserMapper.MapToUserEntity(userModel);
            user.Password = HashHelper.ComputeHash(userModel.Password);

            var result = await _userRepositry.CreateAsync(user);
            if (result.Id > 0)
            {
                await _expenseCategoryService.CreateCategoriesForUserSignup(result.Id); // Create User expense categories while user sign up.

                var userCreated = await _userRepositry.GetByIdAsync(result.Id);
                if (userCreated != null)
                {
                    var userDetails = UserMapper.MapToUserModel(userCreated);
                    var token = new TokenModel()
                    {
                        Token = _tokenService.GenerateToken(userModel),
                        UserDetails = userModel
                    };
                    return ServiceResult<TokenModel>.Ok(token);
                }
            }

            return ServiceResult<TokenModel>.Fail("Something went wrong saving user details!", UserMapper.MapToTokenModel(userModel));
        }

        public async Task<ServiceResult<UserModel>> UpdateUser(int id, UserModel userModel)
        {
            var userResult = await _userRepositry.FirstOrDefaultAsync(u => u.Id == id && u.IsActive);
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
            var userResult = await _userRepositry.FirstOrDefaultAsync(u => u.Id == id && u.IsActive);
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
            var userResult = await _userRepositry.FirstOrDefaultAsync(u => u.Id == id && u.IsActive);
            if (userResult != null)
            {
                return ServiceResult<UserModel>.Ok(UserMapper.MapToUserModel(userResult));
            }

            return ServiceResult<UserModel>.Fail($"Could not find user details for Id = {id}!");
        }

        public async Task<ServiceResult<TokenModel>> SignIn(SigninModel signinModel)
        {
            var userResult = await _userRepositry.FirstOrDefaultAsync(u => (u.Username.ToLower() == signinModel.Username.ToLower()
                            || u.Email.ToLower() == signinModel.Username.ToLower())
                        && u.IsActive);
            if (userResult != null && HashHelper.VerifyHash(signinModel.Password, userResult.Password))
            {
                var userModel = UserMapper.MapToUserModel(userResult);
                var token = new TokenModel()
                {
                    Token = _tokenService.GenerateToken(userModel),
                    UserDetails = userModel
                };
                return ServiceResult<TokenModel>.Ok(token);
            }

            return ServiceResult<TokenModel>.Fail($"Could not find user details for Username = {signinModel.Username} & Password = {signinModel.Password}!");
        }
    }
}
