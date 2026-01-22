using LineControl.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceHistoryViewModel
  {
    public int Id { get; set; }

    [Display(Name = "Item number")]
    public string? ItemNumber { get; set; }

    [Display(Name = "Model")]
    public string? DeviceModel { get; set; }

    [Display(Name = "Manufacturer")]
    public string? Manufacturer { get; set; }

    public List<DeviceHistoryDetailViewModel> HistoryEntries { get; set; } = new List<DeviceHistoryDetailViewModel>();
  }
}
