using ShopCenter.Data.Context;
using ShopCenter.Domain.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Data.Repository.User
{
    public class UserRepository:IUserRepository
    {
        private ShopCenterDBContext _dbContext;
        public UserRepository(ShopCenterDBContext dBContext)
        {
            _dbContext = dBContext;
        }
    }
}
