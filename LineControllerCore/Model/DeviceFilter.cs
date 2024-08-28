using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LineControllerCore.Model
{
  public class DeviceFilter
  {
    [Display(Name = "Item number")]
    public string ItemNumber { get; set; }

    [Display(Name = "Designation")]
    public string Designation { get; set; }

    [Display(Name = "Manufacturer")]
    public string Manufacturer { get; set; }

    [Display(Name = "Model")]
    public string DeviceModel { get; set; }

    [Display(Name = "Measurement range min")]
    public decimal? MeasurementMin { get; set; }

    [Display(Name = "Measurement range max")]
    public decimal? MeasurementMax { get; set; }

    [Display(Name = "Serial number")]
    public string SerialNumber { get; set; }

    [Display(Name = "Accounting number")]
    public string Accounting { get; set; }

    [Display(Name = "Issued to")]
    public int? IssuedToId { get; set; }

    [Display(Name = "all devices user ever had")]
    public bool AllIssuedTo { get; set; }

    [Display(Name = "Issued from")]
    public DateTime? IssueDateFrom { get; set; }

    [Display(Name = "Issued until")]
    public DateTime? IssueDateTo { get; set; }

    [Display(Name = "Inventory number")]
    public string InventoryNumber { get; set; }

    [Display(Name = "Equipment number")]
    public string EquipmentNumber { get; set; }

    [Display(Name = "SAP Material Number")]
    public string MaterialNumber { get; set; }

    [Display(Name = "Accessories")]
    public string Accessories { get; set; }

    [Display(Name = "Remark")]
    public string Comment { get; set; }

    [Display(Name = "Calibration mandatory")]
    public bool? CalibrationRequired { get; set; }

    [Display(Name = "Calibration tester")]
    public string CalibrationTester { get; set; }

    [Display(Name = "Calibration interval")]
    [Range(1, 60, ErrorMessage = "The interval has to be between {0} ... {1}.")]
    public int? CalibrationInterval { get; set; }

    [Display(Name = "Calibration due to")]
    [DataType(DataType.Date)]
    public DateTime? CalibrationDueDate { get; set; }

    [Display(Name = "Integrated in")]
    public string ParentItemNumber { get; set; }

    [Display(Name = "Created from")]
    public DateTime? CreatedDateFrom { get; set; }

    [Display(Name = "Created to")]
    public DateTime? CreatedDateTo { get; set; }

    [Display(Name = "Created by")]
    public int? CreatedById { get; set; }

    [Display(Name = "Single-use")]
    public bool? SingleUse { get; set; }

    [Display(Name = "Standards")]
    public IEnumerable<int> Standards { get; set; } = new List<int>();

    [Display(Name = "Status")]
    public IEnumerable<int> Statuses { get; set; } = new List<int>();

    [Display(Name = "Inventory location")]
    public IEnumerable<int> StoragePlaces { get; set; } = new List<int>();

    [JsonIgnore]
    public IEnumerable<DeviceStatusViewModel> DeviceStatuses { get; set; }
    [JsonIgnore]
    public bool CanViewDevice { get; set; }

    [JsonIgnore]
    public bool CanEditDevice { get; set; }

    [JsonIgnore]
    public bool CanIssue { get; set; }

    [JsonIgnore]
    public bool CanCalibrate { get; set; }

    [JsonIgnore]
    public bool CanViewDeviceHistory { get; set; }

    [JsonIgnore]
    public bool CanViewIssueHistory { get; set; }
  }
}
