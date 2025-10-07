using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  [ExcludeFromCodeCoverage]
  public class DeviceMeasurementChainHierarchy
  {
    public bool IsMeasurementChainChild { get; init; }

    public int DeviceId { get; init; }

    public string ItemNumber { get; set; }

    public int? ParentId { get; init; }

    public int Position { get; init; }

    public DateTime? CalibrationDate { get; init; }

    public int? CalibrationInterval { get; init; }

    public bool HasActiveCalibrationOrder { get; init; }
  }
}
