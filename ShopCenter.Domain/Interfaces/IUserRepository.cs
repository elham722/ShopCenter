using ShopCenter.Domain.Models.User;
using ShopCenter.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Domain.Interfaces
{
    public interface IUserRepository
    {

        #region Crud
        void Save();

        void AddUser(User user);

        void UpdateUser(User user);
        #endregion

        #region Utility

        Task<User> GetUserById(int id);

        User GetUserByIdSynce(int id);

        bool IsExistsEmail(string email);

        bool IsExistsPhoneNumber(string phone);

        Task<User> GetUserByEmail(string email);

        User GetUserByEmailSynce(string email);

        Task<User> GetUserByPhoneNumber(string phone);
        #endregion

        #region UserPanel

        Task<User> IsExistUserForLoginByEmail(string email, string password);

        Task<User> UserWithActiveCode(string activeCode);

        Task<bool> IsExistUserByActivationCode(string activationCode);

        User GetUserInformation(string EmailOrPhoneNumber);

        #endregion

        #region Admin

        Task<List<UsersListViewModel>> GetAllUsers();

        #endregion

    }
}
