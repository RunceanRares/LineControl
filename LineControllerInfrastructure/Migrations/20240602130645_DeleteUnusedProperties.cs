using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUnusedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceClassMode_DeviceClass_DeviceClassId",
                table: "DeviceClassMode");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceClassMode_DeviceClass_DeviceClassId",
                table: "DeviceClassMode",
                column: "DeviceClassId",
                principalTable: "DeviceClass",
                principalColumn: "DeviceClassId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceClassMode_DeviceClass_DeviceClassId",
                table: "DeviceClassMode");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceClassMode_DeviceClass_DeviceClassId",
                table: "DeviceClassMode",
                column: "DeviceClassId",
                principalTable: "DeviceClass",
                principalColumn: "DeviceClassId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
