﻿using Microsoft.EntityFrameworkCore;
using ShopCenter.Application.Convertors;
using ShopCenter.Application.Generators;
using ShopCenter.Application.Security;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Core.Senders;
using ShopCenter.Domain.Interfaces;
using ShopCenter.Domain.Models.User;
using ShopCenter.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
