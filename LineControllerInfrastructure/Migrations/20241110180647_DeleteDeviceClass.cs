using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDeviceClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_DeviceClass_DeviceClassId",
                table: "Device");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceClassMode_DeviceClass_DeviceClassId",
                table: "DeviceClassMode");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceReservation_DeviceClass_DeviceClassId",
                table: "DeviceReservation");

            migrationBuilder.DropTable(
                name: "DeviceClass");

            migrationBuilder.DropIndex(
                name: "IX_DeviceReservation_DeviceClassId",
                table: "DeviceReservation");

            migrationBuilder.DropIndex(
                name: "IX_DeviceClassMode_DeviceClassId",
                table: "DeviceClassMode");

            migrationBuilder.DropIndex(
                name: "IX_Device_DeviceClassId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "DeviceClassId",
                table: "DeviceClassMode");

            migrationBuilder.DropColumn(
                name: "DeviceClassId",
                table: "Device");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceClassId",
                table: "DeviceClassMode",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeviceClassId",
                table: "Device",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeviceClass",
                columns: table => new
                {
                    DeviceClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceModelId = table.Column<int>(type: "int", nullable: false),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceClass", x => x.DeviceClassId);
                    table.ForeignKey(
                        name: "FK_DeviceClass_DeviceModel_DeviceModelId",
                        column: x => x.DeviceModelId,
                        principalTable: "DeviceModel",
                        principalColumn: "DeviceModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceClass_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_DeviceClassId",
                table: "DeviceReservation",
                column: "DeviceClassId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceClassMode_DeviceClassId",
                table: "DeviceClassMode",
                column: "DeviceClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_DeviceClassId",
                table: "Device",
                column: "DeviceClassId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceClass_DeviceModelId",
                table: "DeviceClass",
                column: "DeviceModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceClass_LastChangedUserId",
                table: "DeviceClass",
                column: "LastChangedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_DeviceClass_DeviceClassId",
                table: "Device",
                column: "DeviceClassId",
                principalTable: "DeviceClass",
                principalColumn: "DeviceClassId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceClassMode_DeviceClass_DeviceClassId",
                table: "DeviceClassMode",
                column: "DeviceClassId",
                principalTable: "DeviceClass",
                principalColumn: "DeviceClassId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceReservation_DeviceClass_DeviceClassId",
                table: "DeviceReservation",
                column: "DeviceClassId",
                principalTable: "DeviceClass",
                principalColumn: "DeviceClassId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
