using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeviceClassinDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceClassId",
                table: "Device",
                type: "int",
                nullable: true,
                defaultValue: 0);
            migrationBuilder.Sql(@"
              UPDATE Device
              SET DeviceClassId = 1 
              WHERE DeviceClassId IS NULL
          ");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceClassId",
                table: "Device",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_DeviceClassId",
                table: "Device",
                column: "DeviceClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_DeviceClass_DeviceClassId",
                table: "Device",
                column: "DeviceClassId",
                principalTable: "DeviceClass",
                principalColumn: "DeviceClassId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_DeviceClass_DeviceClassId",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_Device_DeviceClassId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "DeviceClassId",
                table: "Device");
        }
    }
}
