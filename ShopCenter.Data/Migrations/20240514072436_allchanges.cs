using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopCenter.Data.Migrations
{
    /// <inheritdoc />
    public partial class allchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 5, 14, 10, 54, 34, 189, DateTimeKind.Local).AddTicks(3426));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 5, 14, 10, 54, 34, 189, DateTimeKind.Local).AddTicks(3440));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 5, 14, 10, 54, 34, 189, DateTimeKind.Local).AddTicks(3442));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 5, 8, 19, 37, 2, 426, DateTimeKind.Local).AddTicks(266));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 5, 8, 19, 37, 2, 426, DateTimeKind.Local).AddTicks(278));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2024, 5, 8, 19, 37, 2, 426, DateTimeKind.Local).AddTicks(279));
        }
    }
}
