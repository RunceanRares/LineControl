using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeviceClassModeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.CreateTable(
            name: "DeviceClassMode",
            columns: table => new
            {
              Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
              DeviceClassId = table.Column<int>(type: "int", nullable: false),
              MeasurementMin = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
              MeasurementMax = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
              MeasurementUnit = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
              TemperatureMin = table.Column<float>(type: "real", nullable: true),
              TemperatureMax = table.Column<float>(type: "real", nullable: true),
              OutputMin = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
              OutputMax = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
              MaterialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
              OutputUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
              Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
              LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
              LastChangedUserId = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
              table.PrimaryKey("PK_DeviceClassMode", x => x.Id);
              table.ForeignKey(
                  name: "FK_DeviceClassMode_DeviceClass_DeviceClassId",
                  column: x => x.DeviceClassId,
                  principalTable: "DeviceClass",
                  principalColumn: "DeviceClassId",
                  onDelete: ReferentialAction.Restrict);
              table.ForeignKey(
                  name: "FK_DeviceClassMode_User_LastChangedUserId",
                  column: x => x.LastChangedUserId,
                  principalTable: "User",
                  principalColumn: "UserId",
                  onDelete: ReferentialAction.Restrict);
            });

          migrationBuilder.CreateIndex(
                name: "IX_DeviceClassMode_DeviceClassId",
                table: "DeviceClassMode",
                column: "DeviceClassId");

          migrationBuilder.CreateIndex(
              name: "IX_DeviceClassMode_LastChangedUserId",
              table: "DeviceClassMode",
              column: "LastChangedUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.DropTable(
           name: "DeviceClassMode");
        }
    }
}
