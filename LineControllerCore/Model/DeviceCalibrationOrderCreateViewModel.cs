using LineControllerInfrastructure.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceCalibrationOrderCreateViewModel
  {
    public int DeviceId { get; set; }

    [Display(Name = "Item number")]
    public string ItemNumber { get; set; }

    [Display(Name = "Model")]
    public string DeviceModel { get; set; }

    [Display(Name = "Manufacturer")]
    public string Manufacturer { get; set; }

    public AccountingType? AccountingType { get; set; }

    [Display(Name = "Accounting number")]                                                                   
    public string AccountingNumber { get; set; }

    [Display(Name = "Action")]
    [Required(ErrorMessage = "The 'Action' field is required.")]
    public int? ActionId { get; set; }

    [Display(Name = "after completion forward to")]
    public int? ReceiverId { get; set; }

    [Display(Name = "Number of channels")]
    public int? NoChannels { get; set; }

    [Display(Name = "Target date")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTime? TargetDate { get; set; }

    [Display(Name = "Comment")]
    public string Comment { get; set; }

    public IEnumerable<CalibrationOrderActionViewModel> Actions { get; set; }
  }
}
