using LineControllerInfrastructure.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceCalibrationOrderRoot : BaseModel
  {
    [Column("CalibrationOrderRootId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    public AccountingType? AccountingType { get; set; }

    public string AccountingNumber { get; set; }

    public string Comment { get; set; }

    [Required]
    public int NoChannels { get; set; }

    public User Receiver { get; set; }

    [ForeignKey(nameof(ReceiverId))]
    public int? ReceiverId { get; set; }

    public CalibrationAction Action { get; set; }

    [ForeignKey(nameof(Action))]
    public int ActionId { get; set; }

  }
}
