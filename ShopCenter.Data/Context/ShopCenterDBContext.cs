using Microsoft.EntityFrameworkCore;
using ShopCenter.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCenter.Data.Context
{
    public class ShopCenterDBContext : DbContext
    {
        public ShopCenterDBContext(DbContextOptions<ShopCenterDBContext> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
