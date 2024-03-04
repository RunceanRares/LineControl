using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class RoleViewModel
  {
    public const string SysAdmin = "SysAdmin";
    public const string DeviceMaster = "DeviceMaster";
    public const string User = "User";
    public const string API = "API";

    [ScaffoldColumn(false)]
    public int Id { get; set; }
  }
}
