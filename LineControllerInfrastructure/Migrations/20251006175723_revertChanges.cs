using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class revertChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "SendEmail",
                table: "DeviceCalibrationOrder",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

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
                    AccountingType = table.Column<int>(type: "int", nullable: true),
                    AccountingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoChannels = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCalibrationOrderRoot", x => x.CalibrationOrderRootId);
                    table.ForeignKey(
                        name: "FK_DeviceCalibrationOrderRoot_CalibrationAction_ActionId",
                        column: x => x.ActionId,
                        principalTable: "CalibrationAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                principalColumn: "CalibrationOrderRootId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<bool>(
                name: "SendEmail",
                table: "DeviceCalibrationOrder",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
