using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBS.Data.Migrations
{
    /// <inheritdoc />
    public partial class cbsupdateclinicstaffremoverelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Clinics_ClinicId",
                table: "TimeSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Staff_StaffId",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_ClinicId",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "TimeSlots");

            migrationBuilder.AlterColumn<long>(
                name: "StaffId",
                table: "TimeSlots",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Staff_StaffId",
                table: "TimeSlots",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Staff_StaffId",
                table: "TimeSlots");

            migrationBuilder.AlterColumn<long>(
                name: "StaffId",
                table: "TimeSlots",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Staff_StaffId",
                table: "TimeSlots",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");
        }
    }
}
