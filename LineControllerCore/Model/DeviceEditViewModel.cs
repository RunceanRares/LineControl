using LineControllerInfrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace LineControllerCore.Model
{
  public class DeviceEditViewModel
  {
    private int statusId;

    public int Id { get; set; }

    [Display(Name = "Item number")]
    public string ItemNumber { get; set; }

    [Display(Name = "Device class")]
    [Required(ErrorMessage = "The 'Device class' field is required.")]
    public int? DeviceClassId { get; set; }

    [Display(Name = "Integrated in")]
    public string ParentItemNumber { get; set; }

    [Display(Name = "Model")]
    public string DeviceModel { get; set; }

    public bool HasCalibrationOrderOpened { get; set; }
    public int? ParentId { get; set; }

    [Display(Name = "Measurement range")]
    public string MeasurementRange
    {
      get
      {
        return MeasurementMin == null || MeasurementMax == null ? string.Empty : MeasurementRangeViewModel.CreateId(MeasurementMin.Value, MeasurementMax.Value, MeasurementUnit);
      }
    }

    public decimal? MeasurementMin { get; set; }

    public decimal? MeasurementMax { get; set; }

    public string MeasurementUnit { get; set; }

    [Display(Name = "Activity type")]
    public int? ActivityTypeId { get; set; }

    public decimal? ActivityTypeRate { get; set; }

    [Display(Name = "Activity TP Cost Factor")]
    public decimal? ActivityTypePassiveCostFactor { get; set; }

    [Display(Name = "Status")]
    public int StatusId
    {
      get
      {
        if (statusId == DeviceStatus.UsableId && IssueDate != null)
        {
          if (IsCalibrationDue || IsChildCalibrationDue)
          {
            return DeviceStatus.NotUsableIssuedId;
          }
          else
          {
            return DeviceStatus.IssuedId;
          }
        }

        if (statusId == DeviceStatus.UsableId && (IsCalibrationDue || IsChildCalibrationDue))
        {
          return DeviceStatus.NotUsableId;
        }

        if (statusId == DeviceStatus.UsableId && HasReservations)
        {
          return DeviceStatus.ReservedId;
        }

        return statusId;
      }

      set
      {
        statusId = value;
      }
    }

    [Display(Name = "Inventory location")]
    [Required(ErrorMessage = "The 'Inventory location' field is required.")]
    public int? InventoryLocationId { get; set; }

    [Display(Name = "Storage place")]
    [Required(ErrorMessage = "The 'Storage place' field is required.")]
    public int? StoragePlaceId { get; set; }

    [Display(Name = "Inventory number")]
    public string InventoryNumber { get; set; }

    [Display(Name = "Equipment number")]
    public string EquipmentNumber { get; set; }


    [Display(Name = "Serial number")]
    public string SerialNumber { get; set; }

    [Display(Name = "Issue comment")]
    public string IssueComment { get; set; }

    [Display(Name = "Cost factor")]
    public decimal? CostFactor { get; set; }

    [Display(Name = "Accessories")]
    public string Accessories { get; set; }

    [Display(Name = "Remark")]
    public string Comment { get; set; }

    [Display(Name = "Calibration tester")]
    public string CalibrationTester { get; set; }

    [Display(Name = "Calibration date")]
    [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime? CalibrationDate { get; set; }

    [Display(Name = "Calibration due on")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    [UIHint("DateDisabled")]
    public DateTime? CalibrationDue
    {
      get
      {
        if (IsNew || CalibrationDate == null || CalibrationInterval == null)
        {
          return null;
        }

        return CalibrationDate.Value.AddMonths(CalibrationInterval.Value);
      }
    }

    [Display(Name = "Calibration interval in months")]
    [UIHint("CalibrationMonth")]
    public int? CalibrationInterval { get; set; }

    [Display(Name = "Calibration result in %")]
    [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
    public decimal? CalibrationResult { get; set; }

    [Display(Name = "SAP Material Number")]
    public string MaterialNumber { get; set; }
    public bool ParentReservationLock
    {
      get { return ParentId != null; }
    }

    [Display(Name = "Created on")]
    [DisplayFormat(DataFormatString = "{0:G}")]
    [DataType(DataType.DateTime)]
    [UIHint("DateDisabled")]
    public DateTime? CreationDate { get; set; }

    public string CreatedByFirstName { get; set; }

    public string CreatedByLastName { get; set; }

    public string CreatedByDepartment { get; set; }


    [Display(Name = "Created by")]
    public string CreatedBy
    {
      get
      {
        if (IsNew)
        {
          return string.Empty;
        }

        if (string.IsNullOrEmpty(CreatedByFirstName))
        {
          return "---";
        }

        return $"{CreatedByLastName}, {CreatedByFirstName} {CreatedByDepartment}";
      }
    }

    public bool IsNew
    {
      get
      {
        return Id < 1;
      }
    }

    [Display(Name = "Calibration location")]
    public string CalibrationLocation { get; set; }

    public bool IsDisplay { get; set; }

    public bool IsEmpty { get; set; }

    public double MaxFileSize { get; set; }

    [Display(Name = "Issue date")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    [UIHint("DateDisabled")]
    public DateTime? IssueDate { get; set; }

    public string IssuedToFirstName { get; set; }

    public string IssuedToLastName { get; set; }

    public string IssuedToDepartment { get; set; }

    [Display(Name = "Currently issued to")]
    public string IssuedTo
    {
      get
      {
        if (string.IsNullOrEmpty(IssuedToFirstName))
        {
          return string.Empty;
        }

        return $"{IssuedToLastName}, {IssuedToFirstName} {IssuedToDepartment}";
      }
    }

    [Display(Name = "Accounting number")]
    public string AccountingNumber { get; set; }

    public int? IssueId { get; set; }

    public bool CanShowChildren
    {
      get
      {
        return !IsEmpty && !IsNew;
      }
    }

    public bool CanSearch
    {
      get
      {
        return IsEmpty || !IsNew;
      }
    }

    public bool CanEditItemNumber
    {
      get
      {
        return !IsEmpty && IsNew;
      }
    }

    public bool IsEditable
    {
      get
      {
        return !IsDisplay && !IsEmpty;
      }
    }

    public bool IsCalibrationDue { get; set; }

    public bool IsCalibrationStaffOrSysAdmin { get; set; }

    public bool IsAuthorizedOnInventoryLocation { get; set; }

    public bool CanMove
    {
      get
      {
        return (IsCalibrationStaffOrSysAdmin || (IsAuthorizedOnInventoryLocation && (IsEmpty || !IsNew))) && !IsDisplay;
      }
    }

    public bool CanEditStatus
    {
      get
      {
        return IsEditable && ParentId == null;
      }
    }

    public bool CanIntegrate
    {
      get
      {
        // x                               see also DeviceIntegrationService.CheckIntegrationAsync if a status is changed
        return !IsNew && !IsDisplay && new int[] { DeviceStatus.UsableId, DeviceStatus.LockedId, DeviceStatus.InServiceId }.Contains(statusId);
      }
    }
    public bool IsManagerOrSysAdmin { get; set; }


    public bool HasReservations { get; set; }

    /// <summary>
    /// Gets or sets the universal material number or the no measurement range material number.
    /// </summary>
    public string MeasurementRangeMaterialNumber { get; set; }

    public bool HasMeasurementRangeMaterialNumber { get; set; }

    public bool CanEditMaterialNumber
    {
      get
      {
        return IsEditable && !HasMeasurementRangeMaterialNumber;
      }
    }


    public bool CanView
    {
      get { return IsEditable && !IsNew && HasViewRight; }
    }

    public bool HasViewRight { get; set; }

    public bool CanEdit
    {
      get { return !IsEmpty && IsDisplay && HasEditRight; }
    }

    public bool HasEditRight { get; set; }

    public bool CanReserve
    {
      get { return !IsEmpty && IsDisplay && HasReserveRight; }
    }

    public bool HasReserveRight { get; set; }

    public bool CanClone
    {
      get { return !IsEmpty && IsDisplay && HasCloneRight; }
    }

    public bool HasCloneRight
    {
      get { return HasEditRight || IsManagerOrSysAdmin; }
    }

    public bool CanMoveFile
    {
      get { return IsEditable && HasDeviceClassEditRight && !IsNew; }
    }

    public bool HasDeviceClassEditRight { get; set; }

    public bool CanSaveIssue
    {
      get { return !IsEmpty && IsDisplay && IssueId != null; }
    }

    public bool CanExport
    {
      get { return !IsEmpty && IsDisplay; }
    }

    public bool CanIssueHistory
    {
      get { return !IsEmpty && IsDisplay && HasIssueHistoryRight; }
    }

    public bool HasIssueHistoryRight { get; set; }

    public bool IsChildCalibrationDue { get; set; }
  }
}
