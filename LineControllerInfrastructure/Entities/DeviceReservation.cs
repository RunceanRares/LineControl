using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceReservation : BaseModel
  {
    [Column("DeviceReservationId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    public Device Device { get; set; }

    [ForeignKey(nameof(Device))]
    public int DeviceId { get; set; }

    public int? DeviceClassId { get; set; }

    [Column(TypeName = "DECIMAL(18, 4)")]
    public decimal? MeasurementMin { get; set; }

    [Column(TypeName = "DECIMAL(18, 4)")]
    public decimal? MeasurementMax { get; set; }

    [StringLength(450)]
    public string MeasurementUnit { get; set; }
    public DateTime StartDate { get; set; }
    public string AccountingNumber { get; set; }

    public User User { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [ExcludeFromCodeCoverage]
    public ReservationStatus Status { get; set; }

    [ForeignKey(nameof(Status))]
    public int StatusId { get; set; }

    [ExcludeFromCodeCoverage]
    public DeviceIssue Issue { get; set; }

    [ForeignKey(nameof(Issue))]
    public int? IssueId { get; set; }
  }
}
