using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceReservationViewModel
  {
    public int ReservationId { get; set; }

    public int DeviceId { get; set; }

    [Display(Name = "Item number")]
    public string? ItemNumber { get; set; }

    [Display(Name = "Model")]
    public string? DeviceModel { get; set; }

    [Display(Name = "Reserved on")]
    [DisplayFormat(DataFormatString = "{0:G}")]
    [DataType(DataType.DateTime)]
    public DateTime CreationDate { get; set; }

    [Display(Name = "expected period of use")]
    public string? Period { get; set; }

    public string? UserFirstName { get; set; }

    public string? UserLastName { get; set; }

    public string? UserDepartment { get; set; }

    [Display(Name = "Reserved for")]
    public string User
    {
      get
      {
        if (string.IsNullOrEmpty(UserFirstName))
        {
          return string.Empty;
        }

        return $"{UserLastName}, {UserFirstName} {UserDepartment}";
      }
    }

  }
}
