using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeviceClassModeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceCalibrationOrder_Device_DeviceId",
                table: "DeviceCalibrationOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceCalibrationOrder_Device_DeviceId1",
                table: "DeviceCalibrationOrder");

            migrationBuilder.DropIndex(
                name: "IX_DeviceCalibrationOrder_DeviceId1",
                table: "DeviceCalibrationOrder");

            migrationBuilder.DropColumn(
                name: "DeviceId1",
                table: "DeviceCalibrationOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceCalibrationOrder_Device_DeviceId",
                table: "DeviceCalibrationOrder",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceCalibrationOrder_Device_DeviceId",
                table: "DeviceCalibrationOrder");

            migrationBuilder.AddColumn<int>(
                name: "DeviceId1",
                table: "DeviceCalibrationOrder",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCalibrationOrder_DeviceId1",
                table: "DeviceCalibrationOrder",
                column: "DeviceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceCalibrationOrder_Device_DeviceId",
                table: "DeviceCalibrationOrder",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceCalibrationOrder_Device_DeviceId1",
                table: "DeviceCalibrationOrder",
                column: "DeviceId1",
                principalTable: "Device",
                principalColumn: "DeviceId");
        }
    }
}
