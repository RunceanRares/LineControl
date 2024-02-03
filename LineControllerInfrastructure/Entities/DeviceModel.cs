using System.ComponentModel.DataAnnotations.Schema;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceModel : BaseModel
  {
    [Column("DeviceModelId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    public string Name { get; set; }
  }
}