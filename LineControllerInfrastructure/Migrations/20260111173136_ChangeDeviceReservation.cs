using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeviceReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
      migrationBuilder.DropForeignKey(
          name: "FK_DeviceReservation_ReservationPeriod_ReservationPeriodId",
          table: "DeviceReservation");

      // 2. Ștergem DOAR Indexurile care blochează modificarea
      // (Am scos DropForeignKey pentru IssueId care dădea eroare)
      migrationBuilder.DropIndex(
          name: "IX_DeviceReservation_IssueId",
          table: "DeviceReservation");

      //migrationBuilder.DropIndex(
      //    name: "IX_DeviceReservation_ReservationPeriod_ReservationPeriodId",
      //    table: "DeviceReservation");

      // 3. Modificăm coloanele (le facem Nullable)
      migrationBuilder.AlterColumn<DateTime>(
          name: "StartDate",
          table: "DeviceReservation",
          type: "datetime2",
          nullable: true,
          oldClrType: typeof(DateTime),
          oldType: "datetime2");

      migrationBuilder.AlterColumn<int>(
          name: "ReservationPeriodId",
          table: "DeviceReservation",
          type: "int",
          nullable: true,
          oldClrType: typeof(int),
          oldType: "int");

      migrationBuilder.AlterColumn<int>(
          name: "IssueId",
          table: "DeviceReservation",
          type: "int",
          nullable: true,
          oldClrType: typeof(int),
          oldType: "int");

      migrationBuilder.AlterColumn<DateTime>(
          name: "EndDate",
          table: "DeviceReservation",
          type: "datetime2",
          nullable: true,
          oldClrType: typeof(DateTime),
          oldType: "datetime2");

      migrationBuilder.AlterColumn<string>(
          name: "AccountingNumber",
          table: "DeviceReservation",
          type: "nvarchar(max)",
          nullable: true,
          oldClrType: typeof(string),
          oldType: "nvarchar(max)");

      // 4. Recreăm Indexurile la loc
      migrationBuilder.CreateIndex(
          name: "IX_DeviceReservation_IssueId",
          table: "DeviceReservation",
          column: "IssueId");

      migrationBuilder.CreateIndex(
          name: "IX_DeviceReservation_ReservationPeriod_ReservationPeriodId",
          table: "DeviceReservation",
          column: "ReservationPeriodId");

      // 5. Adăugăm FK-ul pentru ReservationPeriod înapoi
      migrationBuilder.AddForeignKey(
          name: "FK_DeviceReservation_ReservationPeriod_ReservationPeriodId",
          table: "DeviceReservation",
          column: "ReservationPeriodId",
          principalTable: "ReservationPeriod",
          principalColumn: "Id");
    }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceReservation_ReservationPeriod_ReservationPeriodId",
                table: "DeviceReservation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "DeviceReservation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReservationPeriodId",
                table: "DeviceReservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IssueId",
                table: "DeviceReservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "DeviceReservation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountingNumber",
                table: "DeviceReservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceReservation_ReservationPeriod_ReservationPeriodId",
                table: "DeviceReservation",
                column: "ReservationPeriodId",
                principalTable: "ReservationPeriod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
