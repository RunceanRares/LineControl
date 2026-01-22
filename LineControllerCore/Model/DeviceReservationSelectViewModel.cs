using LineControllerInfrastructure.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceReservationSelectViewModel
  {
    public int ReservationId { get; set; }

    public int DeviceId { get; set; }

    public int DeviceClassId { get; set; }

    public string? ItemNumber { get; set; }

    public string? Designation { get; set; }

    public string? DeviceModel { get; set; }

    public string? Manufacturer { get; set; }

    [DataType(DataType.Date)]
    public DateTime? CalibrationDueDate
    {
      get
      {
        if (CalibrationDate == null || CalibrationInterval == null)
        {
          return null;
        }

        return CalibrationDate.Value.AddMonths(CalibrationInterval.Value);
      }
    }

    public DateTime? CalibrationDate { get; set; }

    public int? CalibrationInterval { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    public string? Period { get; set; }

    public bool IsAccountingMandatory { get; set; }

    public string? AccountingNumber { get; set; }

    public AccountingType? AccountingType { get; set; }

    public string? AccountingTitle { get; set; }

    public bool IsSelected { get; set; }

    public bool IsSameDevice { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ReturnDate { get; set; }

    public DateTime MaxReturnDate { get; set; }

    public string InventoryLocation
    {
      get { return $"{InventoryLocationName} - {ResponsibleLastName}, {ResponsibleFirstName} {ResponsibleDepartment}"; }
    }

    public string? InventoryLocationName { get; set; }

    public string? ResponsibleFirstName { get; set; }

    public string? ResponsibleLastName { get; set; }

    public string? ResponsibleDepartment { get; set; }

    public bool HasDescendants { get; set; }
  }
}
