using LineControllerInfrastructure.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LineControllerCore.Model
{
  public class DeviceReservationEditViewModel
  {
    public int ReservationId { get; set; }

    public int? DeviceId { get; set; }

    [Display(Name = "Item number")]
    public string ItemNumber { get; set; }

    public int? DeviceClassId { get; set; }

    [Display(Name = "Designation")]
    public string Designation { get; set; }

    [Display(Name = "Model")]
    public string DeviceModel { get; set; }

    [Display(Name = "Manufacturer")]
    public string Manufacturer { get; set; }

    public bool IsUniversal { get; set; }

    public bool HasMultipleMeasurementRanges { get; set; }

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

    public string? MeasurementUnit { get; set; }


    [Display(Name = "Inventory location")]
    [Required(ErrorMessage = "The 'Inventory location' field is required.")]
    public int? InventoryLocationId { get; set; }

    [Display(Name = "Collection date")]
    [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "The 'Collection date' field is required.")]
    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [Display(Name = "expected period of use")]
    [Required(ErrorMessage = "The 'expected period of use' field is required.")]
    public int? PeriodId { get; set; }

    [Display(Name = "Accounting number")]
    public string? AccountingNumber { get; set; }

    public AccountingType AccountingType { get; set; }

    public bool SelectEquivalentRequired { get; set; }

    [JsonIgnore]
    public bool IsMeasurementRangeMandatory
    {
      get { return ReservationId == 0 && DeviceId == null && DeviceClassId != null && !IsUniversal && HasMultipleMeasurementRanges; }
    }

    public bool CanReserve => ReservationId == 0;
    public bool IsEditable => ReservationId != 0;

    public bool IsAccountingMandatory { get; set; }

    public bool CheckCalibrationDue { get; set; }

    public bool HasCalibrationDue { get; set; }

  }
}
