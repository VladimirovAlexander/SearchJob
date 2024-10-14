using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SearchJob.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataTypeOfAtributeSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21e92a86-894e-4b18-9900-5091864740f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9bf0a17-1d93-4fa3-a1f9-c9f627c80469");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalaryTo",
                table: "Jobs",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "SalaryFrom",
                table: "Jobs",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2bbc59a5-8ae5-4fc5-aeaf-e861eb2ad2a0", null, "Admin", "ADMIN" },
                    { "93be61bb-7cfc-49b2-8ad5-8fe401d7b354", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bbc59a5-8ae5-4fc5-aeaf-e861eb2ad2a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93be61bb-7cfc-49b2-8ad5-8fe401d7b354");

            migrationBuilder.AlterColumn<int>(
                name: "SalaryTo",
                table: "Jobs",
                type: "integer",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SalaryFrom",
                table: "Jobs",
                type: "integer",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21e92a86-894e-4b18-9900-5091864740f5", null, "Admin", "ADMIN" },
                    { "f9bf0a17-1d93-4fa3-a1f9-c9f627c80469", null, "User", "USER" }
                });
        }
    }
}
