using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopCenter.Data.Migrations
{
    /// <inheritdoc />
    public partial class category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategorySubs_SubCategorySubId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "SubCategorySubs");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SubCategorySubId",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SubCategorySubId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "MainCategories",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_MainCategories_ParentId",
                table: "MainCategories",
                column: "ParentId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainCategories_MainCategories_ParentId",
                table: "MainCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_MainCategories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_MainCategories_ParentId",
                table: "MainCategories");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "MainCategories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "SubCategorySubId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_SubCategorySubId");

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    NameSubCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_MainCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "MainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCategorySubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    NameSubCategorySub = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategorySubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategorySubs_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 4, 25, 12, 20, 26, 699, DateTimeKind.Local).AddTicks(3430));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 4, 25, 12, 20, 26, 699, DateTimeKind.Local).AddTicks(3446));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 4, 25, 12, 20, 26, 699, DateTimeKind.Local).AddTicks(3447));

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_MainCategoryId",
                table: "SubCategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorySubs_SubCategoryId",
                table: "SubCategorySubs",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategorySubs_SubCategorySubId",
                table: "Products",
                column: "SubCategorySubId",
                principalTable: "SubCategorySubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
