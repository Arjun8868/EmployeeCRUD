using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeCRUD.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class FixRoleConcurrencyStamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f8b0c3e-4d2a-4b5c-9f3e-7c8d9e0f1a2b",
                column: "ConcurrencyStamp",
                value: "1f8b0c3e-4d2a-4b5c-9f3e-7c8d9e0f1a2b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f8b0c3e-4d2a-4b5c-9f3e-7c8d9e0f1a2b",
                column: "ConcurrencyStamp",
                value: "1f8b0c3e-4d2a-4b5c-9f3e-7c8d9e0f1a2b");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f8b0c3e-4d2a-4b5c-9f3e-7c8d9e0f1a2b",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f8b0c3e-4d2a-4b5c-9f3e-7c8d9e0f1a2b",
                column: "ConcurrencyStamp",
                value: null);
        }
    }
}
