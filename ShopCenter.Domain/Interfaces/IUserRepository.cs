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
        void AddUser(User user);
        void Save();

        void UpdateUser(User user);

        Task<User> GetUserById(int id);
        User GetUserByIdSynce(int id);

        Task<User> IsExistUserForLoginByEmail(string email, string password);

        Task<User> GetUserByEmail(string email);
        User GetUserByEmailSynce(string email);

        Task<User> GetUserByPhoneNumber(string phone);

        Task<User> UserWithActiveCode(string activeCode);

        Task<bool> IsExistUserByActivationCode(string activationCode);

        User GetUserInformation(string EmailOrPhoneNumber);


        Task<List<UsersListViewModel>> GetAllUsers();

        bool IsExistsEmail(string email);

        bool IsExistsPhoneNumber(string phone);
    }
}
