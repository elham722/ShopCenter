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
    public class PermissionRepository :IPermissionRepository
    {
        private ShopCenterDBContext _dbContext;
        public PermissionRepository(ShopCenterDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<List<Role>> GetAllRoles()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public bool IsExistRoleById(int roleId)
        {
            return _dbContext.Roles.Any(r => r.Id == roleId);
        }
    }
}
