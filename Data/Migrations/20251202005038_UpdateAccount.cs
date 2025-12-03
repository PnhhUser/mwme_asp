using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Accounts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsActived",
                table: "Accounts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "Accounts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl", "IsActived", "IsOnline", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 2, 0, 50, 38, 171, DateTimeKind.Utc).AddTicks(6645), "", false, false, new DateTime(2025, 12, 2, 0, 50, 38, 171, DateTimeKind.Utc).AddTicks(6649) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsActived",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 11, 30, 3, 54, 11, 460, DateTimeKind.Utc).AddTicks(693), new DateTime(2025, 11, 30, 3, 54, 11, 460, DateTimeKind.Utc).AddTicks(696) });
        }
    }
}
