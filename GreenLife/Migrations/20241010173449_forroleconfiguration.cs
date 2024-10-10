using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenLife.Migrations
{
    /// <inheritdoc />
    public partial class forroleconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a784810-7a30-45ce-ba2b-e657b3265e81");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d658022-a721-4007-9fd2-695a079eba8e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9238dd99-6d96-4b48-8955-e62b8d511c37");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0fbd7d7a-9c8e-4878-b551-b288fce5d043", null, "Administrator", "ADMINISTRATOR" },
                    { "265c1633-493b-4b15-8726-e40e211de033", null, "Conter", "COUNTER" },
                    { "8d373bcf-ab1e-4806-bdde-e41ea140294f", null, "Administrator", "ADMINISTRATOR" },
                    { "d866b15e-9d36-4571-886c-813e65787702", null, "Conter", "COUNTER" },
                    { "e91712a8-70da-4b7f-b091-6f02134aa362", null, "Manager", "MANAGER" },
                    { "f89549f6-c695-4f9b-b743-2c7ea0390fac", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fbd7d7a-9c8e-4878-b551-b288fce5d043");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "265c1633-493b-4b15-8726-e40e211de033");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d373bcf-ab1e-4806-bdde-e41ea140294f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d866b15e-9d36-4571-886c-813e65787702");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e91712a8-70da-4b7f-b091-6f02134aa362");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f89549f6-c695-4f9b-b743-2c7ea0390fac");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6a784810-7a30-45ce-ba2b-e657b3265e81", null, "Conter", "COUNTER" },
                    { "7d658022-a721-4007-9fd2-695a079eba8e", null, "Administrator", "ADMINISTRATOR" },
                    { "9238dd99-6d96-4b48-8955-e62b8d511c37", null, "Manager", "MANAGER" }
                });
        }
    }
}
