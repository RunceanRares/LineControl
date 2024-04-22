using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceFilter
  {
    [JsonIgnore]
    public bool CanViewDevice { get; set; }

    [JsonIgnore]
    public bool CanEditDevice { get; set; }

    [JsonIgnore]
    public bool CanIssue { get; set; }

    [JsonIgnore]
    public bool CanCalibrate { get; set; }

    [JsonIgnore]
    public bool CanViewDeviceHistory { get; set; }

    [JsonIgnore]
    public bool CanViewIssueHistory { get; set; }
  }
}
