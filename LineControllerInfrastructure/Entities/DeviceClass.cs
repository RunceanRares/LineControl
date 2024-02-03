using System.ComponentModel.DataAnnotations.Schema;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceClass : BaseModel
  {
    [Column("DeviceClassId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    [ForeignKey(nameof(DeviceModel))]
    public int DeviceModelId { get; set; }

    public DeviceModel DeviceModel { get; set; }
  }
}