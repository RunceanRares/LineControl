using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceClassViewModel
  {
    public int Id { get; set; }

    [Display(Name = "Model")]
    public string DeviceModelName { get; set; }

    [Display(Name = "Manufacturer")]
    public string ManufacturerName { get; set; }
    public string Name
    {
      get
      {
        // Exemplu: "Samsung - Galaxy S10" sau doar "Galaxy S10"
        return $"{ManufacturerName} {DeviceModelName}".Trim();
      }
    }
  }
}
