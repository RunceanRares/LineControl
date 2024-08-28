using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LineControllerInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalibrationAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCalibrationOrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCalibrationOrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Min = table.Column<int>(type: "int", nullable: false),
                    Max = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationPeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityType",
                columns: table => new
                {
                    ActivityTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CostCenter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PassiveCostFactor = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: true),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityType", x => x.ActivityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLocation",
                columns: table => new
                {
                    CompanyLocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLocation", x => x.CompanyLocationId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyLocationId = table.Column<int>(type: "int", nullable: false),
                    UserRoles = table.Column<int>(type: "int", nullable: false),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.CheckConstraint("CHK_EmailAndDepartment", "[Status] <> 1 OR ([Email] IS NOT NULL AND [Email] <> '' AND [Department] IS NOT NULL AND [Department] <> '')");
                    table.ForeignKey(
                        name: "FK_User_CompanyLocation_CompanyLocationId",
                        column: x => x.CompanyLocationId,
                        principalTable: "CompanyLocation",
                        principalColumn: "CompanyLocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_User_User_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "DeviceModel",
                columns: table => new
                {
                    DeviceModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceModel", x => x.DeviceModelId);
                    table.ForeignKey(
                        name: "FK_DeviceModel_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryLocation",
                columns: table => new
                {
                    InventoryLocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponsibleId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SendManagerEmail = table.Column<bool>(type: "bit", nullable: false),
                    GenerateCharge = table.Column<bool>(type: "bit", nullable: false),
                    ActivityTypeId = table.Column<int>(type: "int", nullable: false),
                    CostFactor = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: true),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLocation", x => x.InventoryLocationId);
                    table.CheckConstraint("CHK_ActivityTypeAndCostFactor", "([ActivityTypeId] IS NULL AND [CostFactor] IS NULL) OR ([ActivityTypeId] IS NOT NULL AND [CostFactor] IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_InventoryLocation_ActivityType_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityType",
                        principalColumn: "ActivityTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryLocation_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryLocation_User_ResponsibleId",
                        column: x => x.ResponsibleId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Deactivated = table.Column<bool>(type: "bit", nullable: false),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Role_User_LastChangedUserId",
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceClass_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoragePlace",
                columns: table => new
                {
                    StoragePlaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyLocationId = table.Column<int>(type: "int", nullable: false),
                    Building = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Floor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomDesignation = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResponsibleId = table.Column<int>(type: "int", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    InventoryLocationId = table.Column<int>(type: "int", nullable: false),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoragePlace", x => x.StoragePlaceId);
                    table.CheckConstraint("CK_StoragePlace_Building_RoomDesignation", "ISNULL([Building], '') + ISNULL([RoomDesignation], '') > ''");
                    table.ForeignKey(
                        name: "FK_StoragePlace_CompanyLocation_CompanyLocationId",
                        column: x => x.CompanyLocationId,
                        principalTable: "CompanyLocation",
                        principalColumn: "CompanyLocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoragePlace_InventoryLocation_InventoryLocationId",
                        column: x => x.InventoryLocationId,
                        principalTable: "InventoryLocation",
                        principalColumn: "InventoryLocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoragePlace_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoragePlace_User_ResponsibleId",
                        column: x => x.ResponsibleId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceClassMode",
                columns: table => new
                {
                    DeviceModeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceClassId = table.Column<int>(type: "int", nullable: false),
                    MeasurementMin = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
                    MeasurementMax = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TemperatureMin = table.Column<float>(type: "real", nullable: true),
                    TemperatureMax = table.Column<float>(type: "real", nullable: true),
                    OutputMin = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
                    OutputMax = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
                    MaterialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceClassMode", x => x.DeviceModeId);
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

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    DeviceClassId = table.Column<int>(type: "int", nullable: false),
                    MeasurementMin = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
                    MeasurementMax = table.Column<decimal>(type: "DECIMAL(18,4)", nullable: true),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    InventoryNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    ActivityTypeId = table.Column<int>(type: "int", nullable: true),
                    CostFactor = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: true),
                    Accessories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipmentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalibrationTester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalibrationLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalibrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CalibrationInterval = table.Column<int>(type: "int", nullable: true),
                    CalibrationResult = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: true),
                    Reservation = table.Column<bool>(type: "bit", nullable: false),
                    SAPOrderNumber = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: false),
                    StoragePlaceId = table.Column<int>(type: "int", nullable: false),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                    table.CheckConstraint("CHK_DeviceActivityTypeAndCostFactor", "([ActivityTypeId] IS NULL) OR ([ActivityTypeId] IS NOT NULL AND [CostFactor] IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_Device_ActivityType_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityType",
                        principalColumn: "ActivityTypeId");
                    table.ForeignKey(
                        name: "FK_Device_DeviceClass_DeviceClassId",
                        column: x => x.DeviceClassId,
                        principalTable: "DeviceClass",
                        principalColumn: "DeviceClassId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Device_DeviceStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DeviceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_Device_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Device_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCalibrationOrder",
                columns: table => new
                {
                    CalibrationOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    CalibrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreviousDeviceStatusId = table.Column<int>(type: "int", nullable: true),
                    Inspector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessingTime = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: true),
                    MeasurementSpan = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: true),
                    CalibrationResult = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: true),
                    SendEmail = table.Column<bool>(type: "bit", nullable: false),
                    RootId = table.Column<int>(type: "int", nullable: false),
                    Edited = table.Column<bool>(type: "bit", nullable: false),
                    IsRoot = table.Column<bool>(type: "bit", nullable: false),
                    DeviceId1 = table.Column<int>(type: "int", nullable: true),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCalibrationOrder", x => x.CalibrationOrderId);
                    table.ForeignKey(
                        name: "FK_DeviceCalibrationOrder_DeviceCalibrationOrderRoot_RootId",
                        column: x => x.RootId,
                        principalTable: "DeviceCalibrationOrderRoot",
                        principalColumn: "CalibrationOrderRootId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceCalibrationOrder_DeviceStatus_PreviousDeviceStatusId",
                        column: x => x.PreviousDeviceStatusId,
                        principalTable: "DeviceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceCalibrationOrder_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceCalibrationOrder_Device_DeviceId1",
                        column: x => x.DeviceId1,
                        principalTable: "Device",
                        principalColumn: "DeviceId");
                    table.ForeignKey(
                        name: "FK_DeviceCalibrationOrder_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_DeviceHistory_User_ModificationUserId",
                        column: x => x.ModificationUserId,
                        principalTable: "User",
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
                    CollectorId = table.Column<int>(type: "int", nullable: true),
                    AccountingType = table.Column<int>(type: "int", nullable: true),
                    AccountingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReturnDatePlanned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnDateActual = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    AvoidDuplicate = table.Column<int>(type: "int", nullable: false, computedColumnSql: "CASE WHEN [ReturnDateActual] IS NULL THEN [DeviceId] ELSE -[DeviceIssueId] END"),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceIssue", x => x.DeviceIssueId);
                    table.ForeignKey(
                        name: "FK_DeviceIssue_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceIssue_User_CollectorId",
                        column: x => x.CollectorId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_DeviceIssue_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_DeviceIssue_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceIssue_User_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCalibrationOrderStatusHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalibrationOrderId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCalibrationOrderStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceCalibrationOrderStatusHistory_DeviceCalibrationOrderStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DeviceCalibrationOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceCalibrationOrderStatusHistory_DeviceCalibrationOrder_CalibrationOrderId",
                        column: x => x.CalibrationOrderId,
                        principalTable: "DeviceCalibrationOrder",
                        principalColumn: "CalibrationOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceCalibrationOrderStatusHistory_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
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
                    InventoryLocationId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    LastChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastChangedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceReservation", x => x.DeviceReservationId);
                    table.ForeignKey(
                        name: "FK_DeviceReservation_DeviceClass_DeviceClassId",
                        column: x => x.DeviceClassId,
                        principalTable: "DeviceClass",
                        principalColumn: "DeviceClassId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceReservation_DeviceIssue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "DeviceIssue",
                        principalColumn: "DeviceIssueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceReservation_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceReservation_InventoryLocation_InventoryLocationId",
                        column: x => x.InventoryLocationId,
                        principalTable: "InventoryLocation",
                        principalColumn: "InventoryLocationId");
                    table.ForeignKey(
                        name: "FK_DeviceReservation_ReservationStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ReservationStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceReservation_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceReservation_User_LastChangedUserId",
                        column: x => x.LastChangedUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceReservation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CalibrationAction",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Calibration" },
                    { 2, "Checking" },
                    { 3, "DMS application" }
                });

            migrationBuilder.InsertData(
                table: "DeviceCalibrationOrderStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 10, "Received" },
                    { 20, "In Progress" },
                    { 30, "Sent Externally" },
                    { 70, "Finished (OK)" },
                    { 80, "Finished (NOK)" },
                    { 90, "Finished (Adjusted)" },
                    { 100, "Finished (OK after Adjustment)" },
                    { 110, "Finished (Scrap)" },
                    { 120, "Returned" }
                });

            migrationBuilder.InsertData(
                table: "DeviceStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 100, "usable" },
                    { 200, "defect" },
                    { 300, "given away/disposed" },
                    { 400, "lost" },
                    { 500, "locked" },
                    { 600, "being serviced" }
                });

            migrationBuilder.InsertData(
                table: "ReservationPeriod",
                columns: new[] { "Id", "Max", "Min", "Name" },
                values: new object[,]
                {
                    { 1, 2, 1, "1 ... 2 days" },
                    { 2, 4, 2, "2 ... 4 days" },
                    { 3, 14, 7, "1 ... 2 weeks" },
                    { 4, 28, 14, "2 ... 4 weeks" },
                    { 5, 42, 21, "3 ... 6 weeks" },
                    { 6, 56, 28, "4 ... 8 weeks" },
                    { 7, 84, 42, "6 ... 12 weeks" },
                    { 8, 112, 56, "8 ... 16 weeks" },
                    { 9, 168, 84, "12 ... 24 weeks" },
                    { 10, null, 168, "more than 24" }
                });

            migrationBuilder.InsertData(
                table: "ReservationStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "Canceled, in accordance with deadline" },
                    { 3, "Canceled, not in accordance with deadline" },
                    { 4, "Not collected" },
                    { 5, "Collected" },
                    { 6, "Alternative rejected or not present" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Deactivated", "LastChangedDate", "LastChangedUserId", "Name" },
                values: new object[,]
                {
                    { 1, false, null, null, "SysAdmin" },
                    { 2, false, null, null, "CalibrationStaff" },
                    { 3, false, null, null, "DeviceManager" },
                    { 4, false, null, null, "User" },
                    { 5, false, null, null, "DeviceMaster" },
                    { 6, false, null, null, "ChargeAdmin" },
                    { 7, false, null, null, "API" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityType_LastChangedUserId",
                table: "ActivityType",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityType_Name",
                table: "ActivityType",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLocation_Code",
                table: "CompanyLocation",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLocation_LastChangedUserId",
                table: "CompanyLocation",
                column: "LastChangedUserId");

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
                name: "IX_Device_ItemNumber",
                table: "Device",
                column: "ItemNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_LastChangedUserId",
                table: "Device",
                column: "LastChangedUserId");

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
                name: "IX_DeviceCalibrationOrder_DeviceId",
                table: "DeviceCalibrationOrder",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCalibrationOrder_DeviceId1",
                table: "DeviceCalibrationOrder",
                column: "DeviceId1");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCalibrationOrder_LastChangedUserId",
                table: "DeviceCalibrationOrder",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCalibrationOrder_PreviousDeviceStatusId",
                table: "DeviceCalibrationOrder",
                column: "PreviousDeviceStatusId");

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

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCalibrationOrderStatusHistory_CalibrationOrderId",
                table: "DeviceCalibrationOrderStatusHistory",
                column: "CalibrationOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCalibrationOrderStatusHistory_LastChangedUserId",
                table: "DeviceCalibrationOrderStatusHistory",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCalibrationOrderStatusHistory_StatusId",
                table: "DeviceCalibrationOrderStatusHistory",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceClass_DeviceModelId",
                table: "DeviceClass",
                column: "DeviceModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceClass_LastChangedUserId",
                table: "DeviceClass",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceClassMode_DeviceClassId",
                table: "DeviceClassMode",
                column: "DeviceClassId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceClassMode_LastChangedUserId",
                table: "DeviceClassMode",
                column: "LastChangedUserId");

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
                name: "IX_DeviceIssue_LastChangedUserId",
                table: "DeviceIssue",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceIssue_RecipientId",
                table: "DeviceIssue",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceIssue_ReturnDateActual",
                table: "DeviceIssue",
                column: "ReturnDateActual")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceModel_LastChangedUserId",
                table: "DeviceModel",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceModel_Name",
                table: "DeviceModel",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_CreatedById",
                table: "DeviceReservation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_DeviceClassId",
                table: "DeviceReservation",
                column: "DeviceClassId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_DeviceId",
                table: "DeviceReservation",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_InventoryLocationId",
                table: "DeviceReservation",
                column: "InventoryLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_IssueId",
                table: "DeviceReservation",
                column: "IssueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_LastChangedUserId",
                table: "DeviceReservation",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_StatusId",
                table: "DeviceReservation",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReservation_UserId",
                table: "DeviceReservation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLocation_ActivityTypeId",
                table: "InventoryLocation",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLocation_LastChangedUserId",
                table: "InventoryLocation",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLocation_ResponsibleId",
                table: "InventoryLocation",
                column: "ResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_LastChangedUserId",
                table: "Role",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlace_CompanyLocationId",
                table: "StoragePlace",
                column: "CompanyLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlace_InventoryLocationId",
                table: "StoragePlace",
                column: "InventoryLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlace_InventoryLocationId_CompanyLocationId_Building_Floor_RoomNumber_RoomDesignation_Place",
                table: "StoragePlace",
                columns: new[] { "InventoryLocationId", "CompanyLocationId", "Building", "Floor", "RoomNumber", "RoomDesignation", "Place" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlace_InventoryLocationId_Default",
                table: "StoragePlace",
                columns: new[] { "InventoryLocationId", "Default" },
                unique: true,
                filter: "[Default] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlace_LastChangedUserId",
                table: "StoragePlace",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlace_ResponsibleId",
                table: "StoragePlace",
                column: "ResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CompanyLocationId",
                table: "User",
                column: "CompanyLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LastChangedUserId",
                table: "User",
                column: "LastChangedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ManagerId",
                table: "User",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PersonalNumber",
                table: "User",
                column: "PersonalNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityType_User_LastChangedUserId",
                table: "ActivityType",
                column: "LastChangedUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyLocation_User_LastChangedUserId",
                table: "CompanyLocation",
                column: "LastChangedUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyLocation_User_LastChangedUserId",
                table: "CompanyLocation");

            migrationBuilder.DropTable(
                name: "DeviceCalibrationOrderStatusHistory");

            migrationBuilder.DropTable(
                name: "DeviceClassMode");

            migrationBuilder.DropTable(
                name: "DeviceHistory");

            migrationBuilder.DropTable(
                name: "DeviceReservation");

            migrationBuilder.DropTable(
                name: "ReservationPeriod");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "DeviceCalibrationOrderStatus");

            migrationBuilder.DropTable(
                name: "DeviceCalibrationOrder");

            migrationBuilder.DropTable(
                name: "DeviceIssue");

            migrationBuilder.DropTable(
                name: "ReservationStatus");

            migrationBuilder.DropTable(
                name: "DeviceCalibrationOrderRoot");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "CalibrationAction");

            migrationBuilder.DropTable(
                name: "DeviceClass");

            migrationBuilder.DropTable(
                name: "DeviceStatus");

            migrationBuilder.DropTable(
                name: "StoragePlace");

            migrationBuilder.DropTable(
                name: "DeviceModel");

            migrationBuilder.DropTable(
                name: "InventoryLocation");

            migrationBuilder.DropTable(
                name: "ActivityType");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "CompanyLocation");
        }
    }
}
