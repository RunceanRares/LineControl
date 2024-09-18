using System.ComponentModel.DataAnnotations;

namespace LineControllerCore.Models
{
  public class DeviceCalibrationOrderViewModel
  {
    [Display(Name = "Calibration order number")]
    public int Id { get; set; }

    public int DeviceId { get; set; }

    [Display(Name = "Calibration location")]
    public int CalibrationLocationId { get; set; }

    [Display(Name = "Device ClassId")]
    public int DeviceClassId { get; set; }

    [Display(Name = "Item number")]
    public string? ItemNumber { get; set; }

    [Display(Name = "Model")]
    public string? DeviceModel { get; set; }

    [Display(Name = "Originator")]
    public int CreatedById { get; set; }

    public string? CreatedByFirstName { get; set; }

    public string? CreatedByLastName { get; set; }

    public string? CreatedByDepartment { get; set; }

    public string CreatedBy
    {
      get
      {
        if (string.IsNullOrEmpty(CreatedByFirstName))
        {
          return string.Empty;
        }

        return $"{CreatedByLastName}, {CreatedByFirstName} {CreatedByDepartment}";
      }
    }

    public string SerialNumber { get; set; }

    [Display(Name = "Received date")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTime ReceivedDate { get; set; }

    [Display(Name = "Action")]
    public int ActionId { get; set; }

    public string? Action { get; set; }

    [Display(Name = "Status")]
    public int StatusId { get; set; }

    public string? Status { get; set; }  

    public string AccountingNumber { get; set; }

    public string ReceiverFirstName { get; set; }

    public string ReceiverLastName { get; set; }

    public string ReceiverDepartment { get; set; }

    public string ReceiverLocationName { get; set; }
    public string LastStatusModifierFirstName { get; set; }

    public string LastStatusModifierLastName { get; set; }

    public string LastStatusModifierDepartment { get; set; }


    [Display(Name = "Status modification date")]
    [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime? LastStatusModificationDate { get; set; }
    public DateTime? CalibrationDate { get; set; }

    [Display(Name = "Inspector")]                                    
    public string Inspector { get; set; }
    [Display(Name = "Test location")]
    public string TestLocation { get; set; }

    public bool HasMeasurementRange { get; set; }

    [Display(Name = "Processing time")]
    [DisplayFormat(DataFormatString = "{0:#.##}", ApplyFormatInEditMode = true)]
    public decimal? ProcessingTime { get; set; }

    [Display(Name = "Measurement span")]
    [DisplayFormat(DataFormatString = "{0:#.###}", ApplyFormatInEditMode = true)]
    public decimal? MeasurementSpan { get; set; }

    public decimal? CalibrationResult { get; set; }


    [Display(Name = "Send email")]
    public bool SendEmail { get; set; }

    [Display(Name = "Comment")]
    public string Comment { get; set; }
    public int RootId { get; set; }

    public bool IsRoot { get; set; }

    public int? CalibrationInterval { get; set; }
  }
}
