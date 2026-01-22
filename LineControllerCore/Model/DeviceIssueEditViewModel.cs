using LineControllerInfrastructure.Entities.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceIssueEditViewModel
  {
    public int IssueId { get; set; }

    public int DeviceId { get; set; }

    [Display(Name = "Item number")]
    public string ItemNumber { get; set; }

    [Required(ErrorMessage = "The 'Recipient' field is required.")]
    [Display(Name = "Recipient")]
    public int? RecipientId { get; set; }

    public bool IsAccountingMandatory { get; set; }

    public bool HasCalibrationDue { get; set; }

    public string? Parent { get; set; }

    public int? ParentId { get; set; }

    public AccountingType? AccountingType { get; set; }

    public string? AccountingTitle { get; set; }

    [Display(Name = "Accounting number")]
    public string? AccountingNumber { get; set; }

    [Display(Name = "Return due date")]
    [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime? ReturnDate { get; set; }

    [Display(Name = "Comment")]
    public string? Comment { get; set; }

    public string? Status { get; set; }

    public bool CanIssue { get; set; } = true;

    public bool CanRetrieve { get; set; }

    public bool? IsRetrieve { get; set; }

    public bool IsSaveRecipient { get; set; }

    public bool IsNew
    {
      get { return IssueId == 0; }
    }

    public bool HasReservations { get; set; }

    public int? SavedRecipientId { get; set; }

    public int? SavedCollectorId { get; set; }

    public string? SavedAccountingNumber { get; set; }

    public string? DisplayName { get; set; }

    public bool HasDeviceViewRight { get; set; }

    public DateTime MinimReturnDate { get; set; }

    public IList<DeviceReservationSelectViewModel> Reservations { get; init; } = new List<DeviceReservationSelectViewModel>();

    public UserReservationsViewModel UserReservations
    {
      get
      {
        return new UserReservationsViewModel()
        {
          DisplayName = DisplayName ?? string.Empty,
          HasDeviceViewRight = HasDeviceViewRight,
          MinimReturnDate = MinimReturnDate,
          Reservations = Reservations,
        };
      }
    }

    public DateTime MaximReturnDate
    {
      get
      {
        DateTime result = Reservations.Where(r => r.IsSelected && r.DeviceId == DeviceId).Select(r => r.MaxReturnDate).FirstOrDefault();
        if (result == default)
        {
          result = new DateTime(2099, 12, 31);
        }

        return result;
      }
    }
  }
}
