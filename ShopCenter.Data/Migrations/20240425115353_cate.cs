using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopCenter.Data.Migrations
{
    /// <inheritdoc />
    public partial class cate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainCategories_MainCategories_ParentId",
                table: "MainCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_MainCategories_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainCategories",
                table: "MainCategories");

            migrationBuilder.RenameTable(
                name: "MainCategories",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_MainCategories_ParentId",
                table: "Categories",
                newName: "IX_Categories_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 4, 25, 15, 23, 52, 997, DateTimeKind.Local).AddTicks(8307));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 4, 25, 15, 23, 52, 997, DateTimeKind.Local).AddTicks(8320));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 4, 25, 15, 23, 52, 997, DateTimeKind.Local).AddTicks(8322));

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "MainCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentId",
                table: "MainCategories",
                newName: "IX_MainCategories_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainCategories",
                table: "MainCategories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 4, 25, 15, 22, 18, 302, DateTimeKind.Local).AddTicks(551));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 4, 25, 15, 22, 18, 302, DateTimeKind.Local).AddTicks(563));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 4, 25, 15, 22, 18, 302, DateTimeKind.Local).AddTicks(564));

            migrationBuilder.AddForeignKey(
                name: "FK_MainCategories_MainCategories_ParentId",
                table: "MainCategories",
                column: "ParentId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MainCategories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
