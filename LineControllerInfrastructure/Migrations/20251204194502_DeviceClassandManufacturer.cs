using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeviceClassandManufacturer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceClassId",
                table: "DeviceClassMode",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    ManufacturerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.ManufacturerId);
                    table.ForeignKey(
                        name: "FK_Manufacturers_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DeviceClass",
                columns: table => new
                {
                    DeviceClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceModelId = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    IsUniversal = table.Column<bool>(type: "bit", nullable: false),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceClass", x => x.DeviceClassId);
                    table.ForeignKey(
                        name: "FK_DeviceClass_DeviceModel_DeviceModelId",
                        column: x => x.DeviceModelId,
                        principalTable: "DeviceModel",
                        principalColumn: "DeviceModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceClass_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "ManufacturerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceClass_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceClassMode_DeviceClassId",
                table: "DeviceClassMode",
                column: "DeviceClassId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceClass_DeviceModelId",
                table: "DeviceClass",
                column: "DeviceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceClass_LastChangedUserId",
                table: "DeviceClass",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceClass_ManufacturerId",
                table: "DeviceClass",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_LastChangedUserId",
                table: "Manufacturers",
                column: "LastChangedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceClassMode_DeviceClass_DeviceClassId",
                table: "DeviceClassMode",
                column: "DeviceClassId",
                principalTable: "DeviceClass",
                principalColumn: "DeviceClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceClassMode_DeviceClass_DeviceClassId",
                table: "DeviceClassMode");

            migrationBuilder.DropTable(
                name: "DeviceClass");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_DeviceClassMode_DeviceClassId",
                table: "DeviceClassMode");

            migrationBuilder.DropColumn(
                name: "DeviceClassId",
                table: "DeviceClassMode");
        }
    }
}
