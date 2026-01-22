using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReservationPeriodDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationPeriodId",
                table: "DeviceReservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_ReservationPeriodId",
                table: "DeviceReservation",
                column: "ReservationPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceReservation_ReservationPeriod_ReservationPeriodId",
                table: "DeviceReservation",
                column: "ReservationPeriodId",
                principalTable: "ReservationPeriod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceReservation_ReservationPeriod_ReservationPeriodId",
                table: "DeviceReservation");

            migrationBuilder.DropIndex(
                name: "IX_DeviceReservation_ReservationPeriodId",
                table: "DeviceReservation");

            migrationBuilder.DropColumn(
                name: "ReservationPeriodId",
                table: "DeviceReservation");
        }
    }
}
