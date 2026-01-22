using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceReserveHierarchyListViewModel
  {
    public int Id { get; init; }

    public int? ParentId { get; init; }

    [Display(Name = "Item number")]
    public string ItemNumber { get; init; }

    [Display(Name = "Designation")]
    public string Designation { get; init; }

    [Display(Name = "Model")]
    public string DeviceModel { get; init; }

    [Display(Name = "Manufacturer")]
    public string Manufacturer { get; init; }

    [Display(Name = "Calibration due on")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTime? CalibrationDue { get; init; }

    [Display(Name = "Inventory location")]
    public string InventoryLocation
    {
      get { return $"{InventoryLocationName} - {ResponsibleLastName}, {ResponsibleFirstName} {ResponsibleDepartment}"; }
    }

    public string InventoryLocationName { get; init; }

    public string ResponsibleFirstName { get; init; }

    public string ResponsibleLastName { get; init; }

    public string ResponsibleDepartment { get; init; }

    public bool IsCalibrationDue { get; init; }

    public bool HasActiveCalibrationOrder { get; init; }

    public bool IsReserved { get; init; }

    public bool IsUniversal { get; init; }

    public DateTime? ReservationStartDate { get; set; }
    public DateTime? ReservationEndDate { get; set; }
    public string ReservedBy { get; set; }

    public bool HasActiveIssue { get; init; }
  }
}
