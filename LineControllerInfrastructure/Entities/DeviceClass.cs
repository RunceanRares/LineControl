using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceClass : BaseModel
  {
    [Column("DeviceClassId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    [ForeignKey(nameof(DeviceModel))]
    public int DeviceModelId { get; set; }

    public DeviceModel DeviceModel { get; set; }

    [ForeignKey(nameof(Manufacturer))]
    public int ManufacturerId { get; set; }

    public Manufacturer Manufacturer { get; set; }

    public bool IsUniversal { get; set; }

    [ExcludeFromCodeCoverage]
    public virtual ICollection<DeviceClassMode> Modes { get; } = new List<DeviceClassMode>();
  }
}
