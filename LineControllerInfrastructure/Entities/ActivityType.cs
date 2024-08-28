using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LineControllerInfrastructure.Entities
{
  public class ActivityType : BaseModel
  {
    [Column("ActivityTypeId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    [Required]
    public string? Code { get; set; }

    public string? Name { get; set; }

    [Required]
    public string? CostCenter { get; set; }

    [Required]
    [Column(TypeName = "DECIMAL(18, 2)")]
    public decimal Rate { get; set; }

    [Column(TypeName = "DECIMAL(18, 3)")]
    public decimal? PassiveCostFactor { get; set; }
  }
}