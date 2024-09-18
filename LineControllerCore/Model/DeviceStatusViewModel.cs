using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceStatusViewModel
  {
    public const string IssuedEN = "issued";

    public const string ReservedEN = "reserved";

    public const string NotUsableEN = "not usable";

    public const string NotUsableIssuedEN = "not usable+issued";

    public int Id { get; set; }

    public string Name { get; set; }
  }
}
