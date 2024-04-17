using ShopCenter.Domain.Models.User;
using ShopCenter.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Application.Services.Interface
{
    public interface IUserServices
    {
        Task<User> GetUserByEmail(string email);

        Task<User> GetUserByPhoneNumber(string phone);

        void RegisterUser(string email,RegisterViewModel register);
        void RegisterUserByPhoneNumber(string phone);
        Task<User> IsExistUserForLogin(string email, LoginViewModel login);

        Task<bool> ActiveUserAccount(string activeCode);

        Task<bool> IsExistUserByActivationCode(string activationCode);
        Task<bool> ResetUserPassword(string activeCode, ResetPasswordViewModel resetPassword);
    }
}
