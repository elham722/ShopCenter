﻿using ShopCenter.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Application.Services.Interface
{
    public interface IPermissionService
    {
        Task<List<Role>> GetAllRoles();
        bool IsExistRoleById(int roleId);
    }
}
