﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopCenter.Data.Context;

#nullable disable

namespace ShopCenter.Data.Migrations
{
    [DbContext(typeof(ShopCenterDBContext))]
    partial class ShopCenterDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShopCenter.Domain.Models.AboutUs.AboutUs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("AboutUs");
                });

            modelBuilder.Entity("ShopCenter.Domain.Models.Product.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ShopCenter.Domain.Models.Product.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShopCenter.Domain.Models.User.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDefaultForNewUsers")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("RoleTiltle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(2024, 5, 2, 14, 23, 39, 425, DateTimeKind.Local).AddTicks(3709),
                            IsDefaultForNewUsers = false,
                            IsDelete = false,
                            RoleTiltle = "مدیر سایت"
                        },
                        new
                        {
                            Id = 2,
                            CreateDate = new DateTime(2024, 5, 2, 14, 23, 39, 425, DateTimeKind.Local).AddTicks(3724),
                            IsDefaultForNewUsers = false,
                            IsDelete = false,
                            RoleTiltle = "دستیار مدیر"
                        },
                        new
                        {
                            Id = 3,
                            CreateDate = new DateTime(2024, 5, 2, 14, 23, 39, 425, DateTimeKind.Local).AddTicks(3725),
                            IsDefaultForNewUsers = true,
                            IsDelete = false,
                            RoleTiltle = "کاربر عادی"
                        });
                });

            modelBuilder.Entity("ShopCenter.Domain.Models.User.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActivationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvatarName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("MessageCode")
                        .HasColumnType("int");

                    b.Property<string>("NationalNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShopCenter.Domain.Models.Product.Category", b =>
                {
                    b.HasOne("ShopCenter.Domain.Models.Product.Category", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("ShopCenter.Domain.Models.Product.Product", b =>
                {
                    b.HasOne("ShopCenter.Domain.Models.Product.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ShopCenter.Domain.Models.User.User", b =>
                {
                    b.HasOne("ShopCenter.Domain.Models.User.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ShopCenter.Domain.Models.Product.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ShopCenter.Domain.Models.User.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
