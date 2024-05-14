using Microsoft.EntityFrameworkCore;
using ShopCenter.Domain.Models.AboutUs;
using ShopCenter.Domain.Models.ContactUs;
using ShopCenter.Domain.Models.Product;
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<User>()
                  .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role() { Id = 1, RoleTiltle = "مدیر سایت", IsDelete = false, IsDefaultForNewUsers = false },
                    new Role() { Id = 2, RoleTiltle = "دستیار مدیر", IsDelete = false, IsDefaultForNewUsers = false },
                    new Role() { Id = 3, RoleTiltle = "کاربر عادی", IsDelete = false, IsDefaultForNewUsers = true }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
