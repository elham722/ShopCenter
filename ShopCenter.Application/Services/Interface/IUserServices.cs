using ShopCenter.Domain.Models.User;
using ShopCenter.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShopCenter.Domain.Models.Common.Enums;

namespace ShopCenter.Application.Services.Interface
{
    public interface IUserServices
    {

        #region Utility

        Task<User> GetUserByEmail(string email);

        User GetUserByEmailSync(string email);

        Task<User> GetUserByPhoneNumber(string phone);
        #endregion

        #region RegisterUser

        void RegisterUser(string email, RegisterViewModel register);
        Task<User> RegisterUserByPhoneNumber(string phone);

        #endregion

        #region LoginUser

        Task<User> IsExistUserForLogin(string email, LoginViewModel login);
        #endregion

        #region ActiveAccount

        Task<bool> ActiveUserAccount(string activeCode);

        #endregion

        #region RessetPassword
        Task<bool> IsExistUserByActivationCode(string activationCode);
        Task<bool> ResetUserPassword(string activeCode, ResetPasswordViewModel resetPassword);

        #endregion

        #region UserPanel

        UserInformationsViewModel GetUserInformationsForShow(string emailOrPhoneNumber);
        User ConfirmUserInformations(string userEmailOrPhoneNumber, string firstName = "", string lastName = "", string nationalNumber = ""
            , string phoneNumber = "", string email = "", string birthDate = "");

        void ChangeUserPassword(string emailOrPhoneNumber, string password);
        #endregion

        #region Admin

        Task<List<UsersListViewModel>> GetUsersList();


        AddOrEditUserByAdminResult AddUserByAdmin(CreateUserViewModel newUser, int roleId);
        AddOrEditUserByAdminResult EditUserByAdmin(EditUserViewModel user, int roleId);
        Task<DetailsUserViewModel> GetDetailsUserForShow(int UserId);

        Task<EditUserViewModel> ShowDetailsForEditUserByAdmin(int UserId);

        Task<bool> DeleteUser(int UserId);
        #endregion

    }
}
