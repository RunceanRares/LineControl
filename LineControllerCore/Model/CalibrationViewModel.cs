using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class CalibrationViewModel
  {
    public int Id { get; set; }

    public int DeviceId { get; set; }

    public int DeviceClassId { get; set; }

    public int LastStatus { get; set; }
  }
}
