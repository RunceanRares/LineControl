using System.ComponentModel.DataAnnotations.Schema;

namespace LineControllerInfrastructure.Entities
{
  public class InventoryLocation : BaseModel
  {
    [Column("InventoryLocationId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    public string Name { get; set; }

    public int ResponsibleId { get; set; }

    public User Responsible { get; set; }

    public bool Active { get; set; }

    public bool SendManagerEmail { get; set; }

    public bool GenerateCharge { get; set; }

    public ActivityType ActivityType { get; set; }

    [ForeignKey(nameof(ActivityType))]
    public int? ActivityTypeId { get; set; }

    [Column(TypeName = "DECIMAL(18, 3)")]
    public decimal? CostFactor { get; set; }

    public virtual ICollection<StoragePlace> StoragePlaces { get; set; } = new List<StoragePlace>();

  }
}