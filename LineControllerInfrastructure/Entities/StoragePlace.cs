using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LineControllerInfrastructure.Entities
{
  public class StoragePlace : BaseModel
  {
    [Column("StoragePlaceId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    public int CompanyLocationId { get; set; }

    public CompanyLocation CompanyLocation { get; set; }

    public string Building { get; set; }

    public string Floor { get; set; }

    public string RoomNumber { get; set; }

    public string RoomDesignation { get; set; }

    public int ResponsibleId { get; set; }

    public User Responsible { get; set; }

    public string Place { get; set; }

    public bool Default { get; set; }

    public int InventoryLocationId { get; set; }

    [ForeignKey(nameof(InventoryLocationId))]
    public InventoryLocation InventoryLocation { get; set; }

    [ExcludeFromCodeCoverage]
    public virtual ICollection<Device> Devices { get; } = new List<Device>();

  }
}
