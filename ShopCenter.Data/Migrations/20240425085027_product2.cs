using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopCenter.Data.Migrations
{
    /// <inheritdoc />
    public partial class product2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 4, 25, 12, 12, 5, 799, DateTimeKind.Local).AddTicks(5636));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 4, 25, 12, 12, 5, 799, DateTimeKind.Local).AddTicks(5650));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 4, 25, 12, 12, 5, 799, DateTimeKind.Local).AddTicks(5651));
        }
    }
}
