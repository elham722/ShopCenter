using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ShopCenter.Application.Convertors;
using ShopCenter.Application.Generators;
using ShopCenter.Application.Security;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Application.StaticTools;
using ShopCenter.Core.Senders;
using ShopCenter.Data.Migrations;
using ShopCenter.Domain.Interfaces;
using ShopCenter.Domain.Models.User;
using ShopCenter.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Application.Services.Implementation
{
    public class UserServices : IUserServices
    {
        private IUserRepository _userRepository;
        private IViewRenderService _viewRenderService;
        public UserServices(IUserRepository userRepository, IViewRenderService viewRenderService)
        {
            _userRepository = userRepository;
            _viewRenderService = viewRenderService;
        }

        public async Task<bool> ActiveUserAccount(string activeCode)
        {
            var user = await _userRepository.UserWithActiveCode(activeCode);
            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ActivationCode = NameGenerator.GenerateUniqName();
            _userRepository.Save();
            return true;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
       
        public async Task<User> GetUserByPhoneNumber(string phone)
        {
            return await _userRepository.GetUserByPhoneNumber(phone);
        }

        public async Task<bool> IsExistUserByActivationCode(string activationCode)
        {
            return await _userRepository.IsExistUserByActivationCode(activationCode);
        }

        public async Task<User> IsExistUserForLogin(string email, LoginViewModel login)
        {
            var hashpassword = PasswordHasher.HashPasswordMD5(login.Password);
            var emailfix = EmailConvertor.FixEmail(email);
            var user = await _userRepository.IsExistUserForLoginByEmail(emailfix, hashpassword);
            return user;

        }

        public void RegisterUser(string email, RegisterViewModel register)
        {
            var user = new User()
            {
                Email = EmailConvertor.FixEmail(email),
                Password = PasswordHasher.HashPasswordMD5(register.Password),
                AvatarName = "Default.png",
                IsActive = false,
                MessageCode = RandomNumberGenerator.GenerateRendomInteger(10000, 99999),
                ActivationCode = NameGenerator.GenerateUniqName(),

            };
            _userRepository.AddUser(user);
            _userRepository.Save();

            #region send email
            string body = _viewRenderService.RenderToStringAsync("User/_ActiveCodeEmail", user);
            SendEmail.Send(email, " فعالسازی حساب کاربری", body);
            #endregion


        }

        public void RegisterUserByPhoneNumber(string phone)
        {
            var user = new User()
            {
                PhoneNumber = phone,
                ActivationCode = NameGenerator.GenerateUniqName(),
                IsActive = true,
                AvatarName = "Default.png",
                MessageCode = RandomNumberGenerator.GenerateRendomInteger(10000, 99999),

            };
            _userRepository.AddUser(user);
            _userRepository.Save();
        }

        
        public async Task<bool> ResetUserPassword(string activeCode,ResetPasswordViewModel resetPassword)
        {
            User user=await _userRepository.UserWithActiveCode(activeCode);
            if (user == null)
            {
                return false;
            }

            string hashNewPassword = PasswordHasher.HashPasswordMD5(resetPassword.NewPassword);
            string activecode = NameGenerator.GenerateUniqName();
            user.Password = hashNewPassword;
            user.ActivationCode = activecode;
            _userRepository.UpdateUser(user);
            _userRepository.Save();
            return true;
        }

        public UserInformationsViewModel GetUserInformationsForShow(string emailOrPhoneNumber)
        {
            var user = _userRepository.GetUserInformation(emailOrPhoneNumber);
            if (user != null)
            {
                var newUserVM= new UserInformationsViewModel()
                {
                    Email=user.Email,
                    PhoneNumber=user.PhoneNumber,
                    BirthDate=user.BirthDate,
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    NationalNumber=user.NationalNumber,
                    
                };
                return newUserVM;
            }
            return null;
        }

        public User ConfirmUserInformations(string userEmailOrPhoneNumber, string firstName = "", string lastName = "", string nationalNumber = "",
            string phoneNumber = "", string email = "", string birthDate = "")
        {
            var user = _userRepository.GetUserInformation(userEmailOrPhoneNumber);
            if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
            {
                user.FirstName = firstName;
                user.LastName = lastName;
            }
            if (!string.IsNullOrEmpty(nationalNumber))
            {
                user.NationalNumber = nationalNumber;
            }
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                user.PhoneNumber = phoneNumber;
            }
            if (!string.IsNullOrEmpty(email))
            {
                user.Email = EmailConvertor.FixEmail(email);
            }
            if (!string.IsNullOrEmpty(birthDate))
            {
                user.BirthDate = DateTime.Parse(birthDate);
            }
        
            _userRepository.Save();
            return user;
        }

        public void ChangeUserPassword(string emailOrPhoneNumber, string password)
        {
            var user = _userRepository.GetUserInformation(emailOrPhoneNumber);
            user.Password = PasswordHasher.HashPasswordMD5(password);
         
            _userRepository.Save();
        }

        public User GetUserByEmailSync(string email)
        {
            return _userRepository.GetUserByEmailSynce(email);
        }


        public async Task<List<UsersListViewModel>> GetUsersList()
        {
            List<UsersListViewModel> users= await _userRepository.GetAllUsers();
            return users;
        }

     
        public async Task<bool> DeleteUser(int UserId)
        {
            User user =await _userRepository.GetUserById(UserId);
            if (user == null)
            {
                return false;
            }
            if (user.AvatarName != "Default.png")
            {

                ImageTools.DeleteImageFromHard(user.AvatarName, "user");
                user.AvatarName = "Default.png";
            }
            user.IsDelete = true;
            user.Email = "Deleted";
            _userRepository.UpdateUser(user);
            _userRepository.Save();

            return true;
        }


        public AddOrEditUserByAdminResult EditUserByAdmin(EditUserViewModel user, int roleId)
        {
            var exituser = _userRepository.GetUserByIdSynce(user.UserId);
            if (exituser == null)
            {
                return AddOrEditUserByAdminResult.UserNotFound;
            }
            if (user.Email != null)
            {
                if (_userRepository.IsExistsEmail(user.Email) && user.Email != exituser.Email)
                {
                    return AddOrEditUserByAdminResult.DuplicateEmail;
                }
            }
            if (user.PhoneNumber != null)
            {

                if (_userRepository.IsExistsPhoneNumber(user.PhoneNumber))
                {
                    return AddOrEditUserByAdminResult.DuplicatePhoneNumber;
                }
            }

            if (user.UserAvatar != null)
            {
                if (!ImageTools.ImageExtensionIsValid(user.UserAvatar))
                {
                    return AddOrEditUserByAdminResult.ImageExensionInvalid;
                }
                if (!ImageTools.ImageSizeIsValid(user.UserAvatar))
                {
                    return AddOrEditUserByAdminResult.ImageSizeInvalid;
                }
                if (user.AvatarName != null && user.AvatarName != "Default.png") { ImageTools.DeleteImageFromHard(user.AvatarName, "user"); }
                string avatar = null;
                ImageTools.AddImageToHard(user.UserAvatar, NameGenerator.GenerateUniqName().ToString(), "user", out avatar);
                user.AvatarName = avatar;
                exituser.AvatarName = user.AvatarName;
            }
            else
            {
                exituser.AvatarName = "Default.png";
            }

            if(user.Password != null)
            {
                exituser.Password = PasswordHasher.HashPasswordMD5(user.Password);
               
            }
            exituser.Id = user.UserId;
            exituser.RoleId = roleId;
            exituser.Email = EmailConvertor.FixEmail(user.Email);
            user.Password = exituser.Password;
            exituser.FirstName = user.FirstName;
            exituser.LastName = user.LastName;
            exituser.PhoneNumber = user.PhoneNumber;
            exituser.IsActive = user.IsActive;

            if (user.CreateActivationCode)
            {
                exituser.ActivationCode = NameGenerator.GenerateUniqName();

                #region send email
                string body = _viewRenderService.RenderToStringAsync("User/_ActiveCodeEmail", exituser);
                SendEmail.Send(user.Email, " فعالسازی حساب کاربری", body);
                #endregion
            }

            _userRepository.UpdateUser(exituser);
            _userRepository.Save();
            return AddOrEditUserByAdminResult.Done;

        }
        public AddOrEditUserByAdminResult AddUserByAdmin(CreateUserViewModel newUser, int roleId)
        {
            
            if ( _userRepository.IsExistsEmail(newUser.Email))
            {
                return AddOrEditUserByAdminResult.DuplicateEmail;
            }
            if(newUser.PhoneNumber != null) { 

            if ( _userRepository.IsExistsPhoneNumber(newUser.PhoneNumber))
            {
                return AddOrEditUserByAdminResult.DuplicatePhoneNumber;
            }
            }
            string avatar;
            if (newUser.UserAvatar != null)
            {
                if (!ImageTools.ImageExtensionIsValid(newUser.UserAvatar))
                {
                    return AddOrEditUserByAdminResult.ImageExensionInvalid;
                }
                if (!ImageTools.ImageSizeIsValid(newUser.UserAvatar))
                {
                    return AddOrEditUserByAdminResult.ImageSizeInvalid;
                }
                ImageTools.AddImageToHard(newUser.UserAvatar, NameGenerator.GenerateUniqName(), "user", out avatar);
            }
            else
            {
                avatar = "Default.png";
            }
          
            var ouser = new User()
            {
                RoleId= roleId,
                Email = EmailConvertor.FixEmail(newUser.Email),
                Password = PasswordHasher.HashPasswordMD5(newUser.Password),
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
              MessageCode= RandomNumberGenerator.GenerateRendomInteger(10000, 99999),
                PhoneNumber = newUser.PhoneNumber,
                AvatarName = avatar,
                IsActive = newUser.IsActive,
                ActivationCode = NameGenerator.GenerateUniqName(),
            };
  
            if (newUser.CreateActivationCode)
            {
                ouser.ActivationCode = NameGenerator.GenerateUniqName();

                #region send email
                string body = _viewRenderService.RenderToStringAsync("User/_ActiveCodeEmail", ouser);
                SendEmail.Send(newUser.Email, " فعالسازی حساب کاربری", body);
                #endregion
            }

            _userRepository.AddUser(ouser);
             _userRepository.Save();
            return AddOrEditUserByAdminResult.Done;
        }

        public async Task<DetailsUserViewModel> GetDetailsUserForShow(int UserId)
        {
            var user = await _userRepository.GetUserById(UserId);
            if (user == null)
            {
                return null;
            }
            var detail = new DetailsUserViewModel()
            {
                AvatarName= user.AvatarName,
                UserId = UserId,
                BirthDate = user.BirthDate,
                Email = user.Email,
                FullName =user.FirstName+ " " +user.LastName,
                NationalNumber = user.NationalNumber,
                PhoneNumber = user.PhoneNumber,
                RegisterDate = user.CreateDate
            };
            return detail;
        }

        public async Task<EditUserViewModel> ShowDetailsForEditUserByAdmin(int UserId)
        {
            var user = await _userRepository.GetUserById(UserId);
            if (user == null)
            {
                return null;
            }
            var detail = new EditUserViewModel()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName, 
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsActive=user.IsActive,
                RoleId=user.RoleId,
                Password=user.Password,
                AvatarName = user.AvatarName,
            };
            return detail;
        }
    }
}
