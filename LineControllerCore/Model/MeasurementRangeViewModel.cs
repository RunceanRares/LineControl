using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class MeasurementRangeViewModel
  {
    private const string Separator = "_";

    public decimal Min { get; set; }

    public decimal Max { get; set; }

    public string Unit { get; set; }

    public string MaterialNumber { get; set; }

    public string Id
    {
      get
      {
        return CreateId(Min, Max, Unit);
      }
    }

     public static string CreateId(decimal min, decimal max, string unit)
    {
      return string.Format(CultureInfo.InvariantCulture, "{0:0.####}{3}{1:0.####}{3}{2}", min, max, unit, Separator);
    }
  }
}
