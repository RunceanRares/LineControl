using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceIssueHierarchyListViewModel
  {
    public int Id { get; set; }

    public int? ParentId { get; set; }

    [Display(Name = "Item number")]
    public string? ItemNumber { get; set; }

    [Display(Name = "Model")]
    public string? DeviceModel { get; set; }

    [Display(Name = "Manufacturer")]
    public string? Manufacturer { get; set; }

    [Display(Name = "Calibration due on")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTime? CalibrationDue { get; set; }

    public bool IsCalibrationDue { get; set; }

    public bool HasActiveCalibrationOrder { get; set; }

    [Display(Name = "Inventory location")]
    public string InventoryLocation
    {
      get { return $"{InventoryLocationName} - {ResponsibleLastName}, {ResponsibleFirstName} {ResponsibleDepartment}"; }
    }

    public string? InventoryLocationName { get; set; }

    public string? ResponsibleFirstName { get; set; }

    public string? ResponsibleLastName { get; set; }

    public string? ResponsibleDepartment { get; set; }
  }
}
