using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeNullableEntitiesRootId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RootId",
                table: "DeviceCalibrationOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RootId",
                table: "DeviceCalibrationOrder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
