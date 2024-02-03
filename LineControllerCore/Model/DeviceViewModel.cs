using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineControl.Models
{
  public class DeviceViewModel
  {
    public int Id { get; set; }

    [Display(Name = "Item number")]
    public string? ItemNumber { get; set; }
    public int? DeviceClassId { get; set; }

    [Display(Name = "Designation")]
    public string? Designation { get; set; }

    [Display(Name = "Model")]
    public string? DeviceModel { get; set; }

    [Display(Name = "Serial number")]
    public string? SerialNumber { get; set; }
    [Display(Name = "SAP Material Number")]
    public string? MaterialNumber { get; set; }

    [Display(Name = "Status")]
    public int StatusId { get; set; }

    [Display(Name = "Status")]
    public string? StatusName { get; set; }
    public decimal? MeasurementMin { get; set; }

    public decimal? MeasurementMax { get; set; }

    public string? MeasurementUnit { get; set; }

    [Display(Name = "Issued on")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTime? IssueDate { get; set; }

    [Display(Name = "Issue comment")]
    public string? IssueComment { get; set; }

    public string? InventoryNumber { get; set; }

    [Column(TypeName = "DECIMAL(18, 3)")]
    public decimal? CostFactor { get; set; }
  }
}
