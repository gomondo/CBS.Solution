using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CBS.Api.Migrations
{
    /// <inheritdoc />
    public partial class ColumnsupdatesforIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a7586f5-bd84-4c71-862c-08e831eb0950");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dabf4ea4-cf0c-4c52-9551-8a73eaa05662");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d9e997f5-cbdf-4ce1-bcb5-93a84a814c33", "1", "Staff", "Staff Member" },
                    { "e3e6db0e-902f-469f-8b19-b423c27e4aa1", "1", "User", "Patient Member" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ca350840-dd45-43e2-ab9a-ac66733da828", "a1cc70c8-4dc3-4576-b648-d10b30d768d1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9e997f5-cbdf-4ce1-bcb5-93a84a814c33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3e6db0e-902f-469f-8b19-b423c27e4aa1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a7586f5-bd84-4c71-862c-08e831eb0950", "1", "User", "Patient Member" },
                    { "dabf4ea4-cf0c-4c52-9551-8a73eaa05662", "1", "Staff", "Staff Member" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6d39eaf0-f110-4b7f-98b0-eed93b851d64", "717462b3-3846-460d-b917-51d274aa5b37" });
        }
    }
}
