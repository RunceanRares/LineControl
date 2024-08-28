using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceCalibrationOrder : BaseModel
  {
    [Column("CalibrationOrderId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    public Device? Device { get; set; }

    [ForeignKey(nameof(Device))]
    public int DeviceId { get; set; }

    public DateTime? CalibrationDate { get; set; }

    public int? PreviousDeviceStatusId { get; set; }

    public string Inspector { get; set; }

    public string TestLocation { get; set; }

    [Column(TypeName = "DECIMAL(18, 3)")]
    public decimal? ProcessingTime { get; set; }

    [Column(TypeName = "DECIMAL(18, 3)")]
    public decimal? MeasurementSpan { get; set; }

    [Column(TypeName = "DECIMAL(18, 3)")]
    public decimal? CalibrationResult { get; set; }

    public bool SendEmail { get; set; }

    public DeviceCalibrationOrderRoot? Root { get; set; }

    [ForeignKey(nameof(Root))]
    public int RootId { get; set; }

    public bool Edited { get; set; }

    public bool IsRoot { get; set; }

    [ExcludeFromCodeCoverage]
    public virtual ICollection<DeviceCalibrationOrderStatusHistory> StatusHistory { get; init; } = new List<DeviceCalibrationOrderStatusHistory>();

  }
}
