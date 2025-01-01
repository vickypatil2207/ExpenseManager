using Azure.Core;
using ExpenseManager.Api.Database.Entities;
using ExpenseManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Api.Service.Mapper
{
    public class UserMapper
    {
        public static void MapToUserEntity(UserModel userModel, ref User user)
        {
            user.Id = userModel.Id;
            user.Firstname = userModel.Firstname;
            user.Lastname = userModel.Lastname;
            user.Email = userModel.Email;
            user.Username = userModel.Username;
            user.DateOfBirth = userModel.DateOfBirth;
            user.Gender = userModel.Gender;
            user.RegistrationDate = userModel.RegistrationDate ?? DateTime.Now;
            user.IsActive = userModel.IsActive;
        }

        public static User MapToUserEntity(UserModel userModel)
        {
            var user = new User();
            MapToUserEntity(userModel, ref user);
            return user;
        }

        public static UserModel MapToUserModel(User user)
        {
            return new UserModel()
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                RegistrationDate = user.RegistrationDate,
                IsActive = user.IsActive,
            };
        }
    }
}
