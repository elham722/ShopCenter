using Microsoft.EntityFrameworkCore;
using ShopCenter.Data.Context;
using ShopCenter.Domain.Interfaces;
using ShopCenter.Domain.Models.User;
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

        public  void AddUser(User user)
        {
            _dbContext.Users.Add(user);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
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
    }
}
