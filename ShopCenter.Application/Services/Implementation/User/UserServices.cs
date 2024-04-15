using ShopCenter.Application.Services.Interface.User;
using ShopCenter.Domain.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Application.Services.Implementation.User
{
    public class UserServices :IUserServices
    {
        private IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
