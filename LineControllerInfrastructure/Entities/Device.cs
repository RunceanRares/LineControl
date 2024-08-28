﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LineControllerInfrastructure.Entities
{
  public class Device : BaseModel
  {
    [Column("DeviceId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    [Required]
    public string? ItemNumber { get; set; }

    public Device? Parent { get; set; }

    [ForeignKey(nameof(Parent))]
    public int? ParentId { get; set; }

    public DateTime CreationDate { get; set; }

    [ForeignKey(nameof(CreatedBy))]
    public int? CreatedById { get; set; }

    public User? CreatedBy { get; set; }

    public DeviceClass? DeviceClass { get; set; }

    [ForeignKey(nameof(DeviceClass))]
    [Required]
    public int DeviceClassId { get; set; }

    [Column(TypeName = "DECIMAL(18, 4)")]
    public decimal? MeasurementMin { get; set; }

    [Column(TypeName = "DECIMAL(18, 4)")]
    public decimal? MeasurementMax { get; set; }

    [StringLength(450)]
    public string? MeasurementUnit { get; set; }

    public string? InventoryNumber { get; set; }

    public string? SerialNumber { get; set; }

    public DeviceStatus Status { get; set; }

    [ForeignKey(nameof(Status))]
    [Required]
    public int StatusId { get; set; }

    public ActivityType? ActivityType { get; set; }

    [ForeignKey(nameof(ActivityType))]
    public int? ActivityTypeId { get; set; }

    [Column(TypeName = "DECIMAL(18, 3)")]
    public decimal? CostFactor { get; set; }

    public string? Accessories { get; set; }

    public string? Comment { get; set; }

    public string EquipmentNumber { get; set; }

    public string CalibrationTester { get; set; }

    public string CalibrationLocation { get; set; }

    public DateTime? CalibrationDate { get; set; }

    public int? CalibrationInterval { get; set; }

    [Column(TypeName = "DECIMAL(18, 3)")]
    public decimal? CalibrationResult { get; set; }
    
    public bool Reservation { get; set; }

    [Column(TypeName = "DECIMAL(18, 3)")]
    public decimal MaterialNumber { get; set; }
    public StoragePlace StoragePlace { get; set; }

    [ForeignKey(nameof(StoragePlace))]
    [Required]
    public int StoragePlaceId { get; set; }

    [ExcludeFromCodeCoverage]
    public virtual ICollection<Device> Children { get; } = new List<Device>();

    [ExcludeFromCodeCoverage]
    public virtual ICollection<DeviceIssue> Issues { get; init; } = new List<DeviceIssue>();

    [ExcludeFromCodeCoverage]
    public virtual ICollection<DeviceReservation> Reservations { get; init; } = new List<DeviceReservation>();

    [ExcludeFromCodeCoverage]
    public virtual ICollection<DeviceCalibrationOrder> CalibrationOrders { get; } = new List<DeviceCalibrationOrder>();

  }
}
