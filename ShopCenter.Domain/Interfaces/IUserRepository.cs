using ShopCenter.Domain.Models.User;
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

        Task<User> IsExistUserForLoginByEmail(string email, string password);

        Task<User> GetUserByEmail(string email);

        Task<User> GetUserByPhoneNumber(string phone);

        Task<User> UserWithActiveCode(string activeCode);

        Task<bool> IsExistUserByActivationCode(string activationCode);
    }
}
