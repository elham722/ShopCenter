using Microsoft.EntityFrameworkCore;
using ShopCenter.Data.Context;
using ShopCenter.Domain.Interfaces;
using ShopCenter.Domain.Models.User;
using ShopCenter.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private ShopCenterDBContext _dbContext;
        public UserRepository(ShopCenterDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public void AddUser(User user)
        {
            if (user.RoleId == 0)
            {
                user.RoleId = _dbContext.Roles.Single(r => r.IsDefaultForNewUsers).Id;
            }
            
            _dbContext.Users.Add(user);
        }

        public  void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public User GetUserByEmailSynce(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public async Task<User> GetUserByPhoneNumber(string phone)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
        }

        public void UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
        }

        public async Task<User> IsExistUserForLoginByEmail(string email, string password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User> UserWithActiveCode(string activeCode)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.ActivationCode == activeCode);
        }

        public async Task<bool> IsExistUserByActivationCode(string activationCode)
        {
            return await _dbContext.Users.AnyAsync(u => u.ActivationCode == activationCode); 
        }

        public  User GetUserInformation(string EmailOrPhoneNumber)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == EmailOrPhoneNumber || u.PhoneNumber == EmailOrPhoneNumber);
        }

        public async Task<List<UsersListViewModel>> GetAllUsers()
        {
            return await _dbContext.Users.Where(u => !u.IsDelete).Select(u => new UsersListViewModel()
            {
                UserId = u.Id,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                FullName=u.FirstName + " " + u.LastName,
                IsActive=u.IsActive,
                Role = u.Role
            }).OrderByDescending(u => u.UserId).ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

       public User GetUserByIdSynce(int id)
        {
            return  _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public  bool IsExistsEmail(string email)
        {
            return  _dbContext.Users.Any(u => u.Email == email);
        }

        public  bool IsExistsPhoneNumber(string phone)
        {
            return  _dbContext.Users.Any(u => u.PhoneNumber == phone);
        }
    }
}
