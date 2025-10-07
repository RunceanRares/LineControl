using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace LineControllerInfrastructure.Entities
{
  public class DeviceClassMode : BaseModel
  {
    public int Id {  get; set; }

    [Column(TypeName = "DECIMAL(18, 4)")]
    public decimal? MeasurementMin { get; set; }

    [Column(TypeName = "DECIMAL(18, 4)")]
    public decimal? MeasurementMax { get; set; }

    [StringLength(450)]
    public string MeasurementUnit { get; set; }

    public float? TemperatureMin { get; set; }

    public float? TemperatureMax { get; set; }

    [Column(TypeName = "DECIMAL(18, 4)")]
    public decimal? OutputMin { get; set; }

    [Column(TypeName = "DECIMAL(18, 4)")]
    public decimal? OutputMax { get; set; }

    public string MaterialNumber { get; set; }

    public string Description {  get; set; }

    public string OutputUnit { get; set; }

    public DateTime? LastChangedDate { get; set; }

    [ForeignKey(nameof(LastChangedUser))]
    public int? LastChangedUserId { get; set; }

    public User? LastChangedUser { get; set; }
  }
}
