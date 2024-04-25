using Microsoft.Extensions.DependencyInjection;
using ShopCenter.Application.Convertors;
using ShopCenter.Application.Services.Implementation;
using ShopCenter.Application.Services.Interface;
using ShopCenter.Data.Repository;
using ShopCenter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.IoC.Dependency
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Repositories

            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IPermissionRepository,PermissionRepository>();

            #endregion


            #region Services
            services.AddScoped<IViewRenderService, RenderViewToString>();
            services.AddScoped<IUserServices,UserServices>();
            services.AddScoped<IPermissionService, PermissionService>();
            #endregion

        }

    }
}
