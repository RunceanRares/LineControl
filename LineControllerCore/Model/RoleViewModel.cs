using System.ComponentModel.DataAnnotations;

namespace LineControllerCore.Model
{
  public class RoleViewModel
  {
    public const string SysAdmin = "SysAdmin";
    public const string DeviceMaster = "DeviceMaster";
    public const string User = "User";
    public const string API = "API";
    public const string DeviceManager = "DeviceManager";
    public const string CalibrationStaff = "CalibrationStaff";
    public const string ChargeAdmin = "ChargeAdmin";

    [ScaffoldColumn(false)]
    public int Id { get; set; }

    public string Name { get; set; }

    [ScaffoldColumn(false)]
    [Display(Name = "Deactivated")]
    public bool Deactivated { get; set; }
  }
}
