using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablesForEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceModels",
                columns: table => new
                {
                    DeviceModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceModels", x => x.DeviceModelId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryLocations",
                columns: table => new
                {
                    InventoryLocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponsibleId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SendManagerEmail = table.Column<bool>(type: "bit", nullable: false),
                    GenerateCharge = table.Column<bool>(type: "bit", nullable: false),
                    ActivityTypeId = table.Column<int>(type: "int", nullable: false),
                    CostFactor = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLocations", x => x.InventoryLocationId);
                    table.ForeignKey(
                        name: "FK_InventoryLocations_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "ActivityTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryLocations_Users_ResponsibleId",
                        column: x => x.ResponsibleId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Min = table.Column<int>(type: "int", nullable: false),
                    Max = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Deactivated = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceClasses",
                columns: table => new
                {
                    DeviceClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceModelId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceClasses", x => x.DeviceClassId);
                    table.ForeignKey(
                        name: "FK_DeviceClasses_DeviceModels_DeviceModelId",
                        column: x => x.DeviceModelId,
                        principalTable: "DeviceModels",
                        principalColumn: "DeviceModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoragePlace",
                columns: table => new
                {
                    StoragePlaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyLocationId = table.Column<int>(type: "int", nullable: false),
                    Building = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomDesignation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponsibleId = table.Column<int>(type: "int", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryLocationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoragePlace", x => x.StoragePlaceId);
                    table.ForeignKey(
                        name: "FK_StoragePlace_CompanyLocations_CompanyLocationId",
                        column: x => x.CompanyLocationId,
                        principalTable: "CompanyLocations",
                        principalColumn: "CompanyLocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoragePlace_InventoryLocations_InventoryLocationId",
                        column: x => x.InventoryLocationId,
                        principalTable: "InventoryLocations",
                        principalColumn: "InventoryLocationId");
                    table.ForeignKey(
                        name: "FK_StoragePlace_Users_ResponsibleId",
                        column: x => x.ResponsibleId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    DeviceClassId = table.Column<int>(type: "int", nullable: false),
                    MeasurementMin = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
                    MeasurementMax = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    InventoryNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    ActivityTypeId = table.Column<int>(type: "int", nullable: true),
                    CostFactor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Accessories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipmentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalibrationTester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalibrationLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalibrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CalibrationInterval = table.Column<int>(type: "int", nullable: true),
                    CalibrationResult = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: true),
                    Reservation = table.Column<bool>(type: "bit", nullable: false),
                    MaterialNumber = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StoragePlaceId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Device_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "ActivityTypeId");
                    table.ForeignKey(
                        name: "FK_Device_DeviceClasses_DeviceClassId",
                        column: x => x.DeviceClassId,
                        principalTable: "DeviceClasses",
                        principalColumn: "DeviceClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Device_DeviceStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DeviceStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Device_Device_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Device",
                        principalColumn: "DeviceId");
                    table.ForeignKey(
                        name: "FK_Device_StoragePlace_StoragePlaceId",
                        column: x => x.StoragePlaceId,
                        principalTable: "StoragePlace",
                        principalColumn: "StoragePlaceId");
                    table.ForeignKey(
                        name: "FK_Device_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceHistory",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationUserId = table.Column<int>(type: "int", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceHistory", x => new { x.DeviceId, x.ModificationUserId, x.ModificationDate });
                    table.ForeignKey(
                        name: "FK_DeviceHistory_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceHistory_Users_ModificationUserId",
                        column: x => x.ModificationUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceIssue",
                columns: table => new
                {
                    DeviceIssueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    CollectorId = table.Column<int>(type: "int", nullable: false),
                    AccountingType = table.Column<int>(type: "int", nullable: true),
                    AccountingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReturnDatePlanned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnDateActual = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceIssue", x => x.DeviceIssueId);
                    table.ForeignKey(
                        name: "FK_DeviceIssue_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceIssue_Users_CollectorId",
                        column: x => x.CollectorId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_DeviceIssue_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_DeviceIssue_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "DeviceReservation",
                columns: table => new
                {
                    DeviceReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    DeviceClassId = table.Column<int>(type: "int", nullable: true),
                    MeasurementMin = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
                    MeasurementMax = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceReservation", x => x.DeviceReservationId);
                    table.ForeignKey(
                        name: "FK_DeviceReservation_DeviceIssue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "DeviceIssue",
                        principalColumn: "DeviceIssueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceReservation_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId");
                    table.ForeignKey(
                        name: "FK_DeviceReservation_ReservationStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ReservationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceReservation_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Device_ActivityTypeId",
                table: "Device",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_CreatedById",
                table: "Device",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Device_DeviceClassId",
                table: "Device",
                column: "DeviceClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_ParentId",
                table: "Device",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_StatusId",
                table: "Device",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_StoragePlaceId",
                table: "Device",
                column: "StoragePlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceClasses_DeviceModelId",
                table: "DeviceClasses",
                column: "DeviceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHistory_ModificationUserId",
                table: "DeviceHistory",
                column: "ModificationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceIssue_CollectorId",
                table: "DeviceIssue",
                column: "CollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceIssue_CreatedById",
                table: "DeviceIssue",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceIssue_DeviceId",
                table: "DeviceIssue",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceIssue_RecipientId",
                table: "DeviceIssue",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_DeviceId",
                table: "DeviceReservation",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_IssueId",
                table: "DeviceReservation",
                column: "IssueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_StatusId",
                table: "DeviceReservation",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_UserId",
                table: "DeviceReservation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLocations_ActivityTypeId",
                table: "InventoryLocations",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLocations_ResponsibleId",
                table: "InventoryLocations",
                column: "ResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlace_CompanyLocationId",
                table: "StoragePlace",
                column: "CompanyLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlace_InventoryLocationId",
                table: "StoragePlace",
                column: "InventoryLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlace_ResponsibleId",
                table: "StoragePlace",
                column: "ResponsibleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceHistory");

            migrationBuilder.DropTable(
                name: "DeviceReservation");

            migrationBuilder.DropTable(
                name: "ReservationPeriods");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "DeviceIssue");

            migrationBuilder.DropTable(
                name: "ReservationStatuses");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "DeviceClasses");

            migrationBuilder.DropTable(
                name: "DeviceStatuses");

            migrationBuilder.DropTable(
                name: "StoragePlace");

            migrationBuilder.DropTable(
                name: "DeviceModels");

            migrationBuilder.DropTable(
                name: "InventoryLocations");
        }
    }
}
