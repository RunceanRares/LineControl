using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceStatusViewModel
  {
    public const string IssuedDE = "ausgegeben";
    public const string IssuedEN = "issued";

    public const string ReservedDE = "reserviert";
    public const string ReservedEN = "reserved";

    public const string NotUsableDE = "nicht verwendbar";
    public const string NotUsableEN = "not usable";

    public const string NotUsableIssuedDE = "nicht verwendbar+ausgegeben";
    public const string NotUsableIssuedEN = "not usable+issued";

    public int Id { get; set; }

    public string Name { get; set; }
  }
}
