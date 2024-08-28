using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceClass : BaseModel
  {
    [Column("DeviceClassId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    [ForeignKey(nameof(DeviceModel))]
    public int DeviceModelId { get; set; }


    public DeviceModel? DeviceModel { get; set; }

    [ExcludeFromCodeCoverage]
    public virtual ICollection<DeviceClassMode> Modes { get; } = new List<DeviceClassMode>();
  }
}