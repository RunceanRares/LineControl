using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullsInIssueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeviceReservation_IssueId",
                table: "DeviceReservation");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_IssueId",
                table: "DeviceReservation",
                column: "IssueId",
                unique: true,
                filter: "[IssueId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeviceReservation_IssueId",
                table: "DeviceReservation");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_IssueId",
                table: "DeviceReservation",
                column: "IssueId",
                unique: true);
        }
    }
}
