using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class CalibrationLocationViewModel
  {
    public int Id { get; set; }                // ID-ul din CalibrationLocation
    public string? Name { get; set; }          // Numele locației din CompanyLocation
    public string? Code { get; set; }          // Codul locației de calibrare
    public string? CostCenter { get; set; }
  }
}
