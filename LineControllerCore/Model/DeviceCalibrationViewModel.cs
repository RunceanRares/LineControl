using System.ComponentModel.DataAnnotations;

namespace LineControllerCore.Models
{
  public class DeviceCalibrationViewModel
  {
    [Display(Name = "Calibration order number")]
    public int Id { get; set; }

    public int DeviceId { get; set; }

    [Display(Name = "Calibration location")]
    public int CalibrationLocationId { get; set; }

    [Display(Name = "Item number")]
    public string? ItemNumber { get; set; }

    [Display(Name = "Designation")]
    public string? Designation { get; set; }

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
  }
}
