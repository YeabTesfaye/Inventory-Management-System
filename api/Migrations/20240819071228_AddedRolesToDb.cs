using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25137149-c354-474b-aa1e-c7b38e37620b", null, "Administrator", "ADMINISTRATOR" },
                    { "3ff8df89-1156-42a6-8596-783dd0b6e9e3", null, "Manager", "MANAGER" }
                });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: new Guid("b5a0a9ef-2e1e-4eb7-8e16-0402fa19e752"),
                column: "OrderDate",
                value: new DateTime(2024, 8, 19, 10, 12, 27, 850, DateTimeKind.Local).AddTicks(762));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25137149-c354-474b-aa1e-c7b38e37620b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ff8df89-1156-42a6-8596-783dd0b6e9e3");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: new Guid("b5a0a9ef-2e1e-4eb7-8e16-0402fa19e752"),
                column: "OrderDate",
                value: new DateTime(2024, 8, 19, 10, 9, 25, 243, DateTimeKind.Local).AddTicks(8657));
        }
    }
}
