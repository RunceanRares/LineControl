using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteRoot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceCalibrationOrder_DeviceCalibrationOrderRoot_RootId",
                table: "DeviceCalibrationOrder");

            migrationBuilder.DropTable(
                name: "DeviceCalibrationOrderRoot");

            migrationBuilder.DropIndex(
                name: "IX_DeviceCalibrationOrder_RootId",
                table: "DeviceCalibrationOrder");

            migrationBuilder.DropColumn(
                name: "RootId",
                table: "DeviceCalibrationOrder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RootId",
                table: "DeviceCalibrationOrder",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeviceCalibrationOrderRoot",
                columns: table => new
                {
                    CalibrationOrderRootId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionId = table.Column<int>(type: "int", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: true),
                    AccountingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountingType = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoChannels = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCalibrationOrderRoot", x => x.CalibrationOrderRootId);
                    table.ForeignKey(
                        name: "FK_DeviceCalibrationOrderRoot_CalibrationAction_ActionId",
                        column: x => x.ActionId,
                        principalTable: "CalibrationAction",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeviceCalibrationOrderRoot_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceCalibrationOrderRoot_User_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCalibrationOrder_RootId",
                table: "DeviceCalibrationOrder",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCalibrationOrderRoot_ActionId",
                table: "DeviceCalibrationOrderRoot",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCalibrationOrderRoot_LastChangedUserId",
                table: "DeviceCalibrationOrderRoot",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCalibrationOrderRoot_ReceiverId",
                table: "DeviceCalibrationOrderRoot",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceCalibrationOrder_DeviceCalibrationOrderRoot_RootId",
                table: "DeviceCalibrationOrder",
                column: "RootId",
                principalTable: "DeviceCalibrationOrderRoot",
                principalColumn: "CalibrationOrderRootId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
