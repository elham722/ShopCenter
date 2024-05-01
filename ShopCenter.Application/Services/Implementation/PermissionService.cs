using ShopCenter.Application.Services.Interface;
using ShopCenter.Data.Repository;
using ShopCenter.Domain.Interfaces;
using ShopCenter.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Application.Services.Implementation
{
    public class PermissionService: IPermissionService
    {
        #region Ctor
        private IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        #endregion

        public async Task<List<Role>> GetAllRoles()
        {
            return await _permissionRepository.GetAllRoles();
        }

        public bool IsExistRoleById(int roleId)
        {
            return _permissionRepository.IsExistRoleById(roleId);
        }
    }
}
