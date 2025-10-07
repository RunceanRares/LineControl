using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceChildListViewModel
  {
    public int Id { get; init; }

    public int ParentId { get; init; }

    [Display(Name = "Item number")]
    public string ItemNumber { get; init; }

    [Display(Name = "Designation")]
    public string Designation { get; init; }

    [Display(Name = "Model")]
    public string DeviceModel { get; init; }

    [Display(Name = "Manufacturer")]
    public string Manufacturer { get; init; }

    public bool IsCalibrationDue { get; init; }

    public bool HasActiveCalibrationOrder { get; init; }
  }
}
