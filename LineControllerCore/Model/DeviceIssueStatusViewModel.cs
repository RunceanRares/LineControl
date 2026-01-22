using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceIssueStatusViewModel
  {
    public bool IsIssued { get; set; }
    public bool CanRetrieve { get; set; }
    public int LastIssueId { get; set; }
  }
}
