using Microsoft.Extensions.DependencyInjection;
using ShopCenter.Application.Services.Implementation.User;
using ShopCenter.Application.Services.Interface.User;
using ShopCenter.Data.Repository.User;
using ShopCenter.Domain.Interfaces.User;
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

            #endregion


            #region Services

            services.AddScoped<IUserServices,UserServices>();

            #endregion

        }

    }
}
