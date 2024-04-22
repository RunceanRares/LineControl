using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class ActivityTypeSelectViewModel
  {
    public int Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string CostCenter { get; set; }

    public decimal? Rate { get; set; }

    public decimal? PassiveCostFactor { get; set; }
  }
}
