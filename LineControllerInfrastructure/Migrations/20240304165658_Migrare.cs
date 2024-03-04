using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migrare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyLocations",
                columns: table => new
                {
                    CompanyLocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLocations", x => x.CompanyLocationId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CompanyLocationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_CompanyLocations_CompanyLocationId",
                        column: x => x.CompanyLocationId,
                        principalTable: "CompanyLocations",
                        principalColumn: "CompanyLocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyLocationId",
                table: "Users",
                column: "CompanyLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ManagerId",
                table: "Users",
                column: "ManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CompanyLocations");
        }
    }
}
