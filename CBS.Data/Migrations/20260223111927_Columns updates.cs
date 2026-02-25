using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBS.Data.Migrations
{
    /// <inheritdoc />
    public partial class Columnsupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClinicId",
                table: "TimeSlots",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_ClinicId",
                table: "TimeSlots",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Clinics_ClinicId",
                table: "TimeSlots",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Clinics_ClinicId",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_ClinicId",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "TimeSlots");
        }
    }
}
