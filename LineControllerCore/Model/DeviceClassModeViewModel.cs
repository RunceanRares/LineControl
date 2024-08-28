using System.ComponentModel.DataAnnotations;

namespace LineControllerCore.Model
{
  public class DeviceClassModeViewModel
  {
    public int ModeId { get; set; }

    public string Measurement
    {
      get
      {
        return $"{MeasurementMin:0.####} ... {MeasurementMax:0.####} {MeasurementUnit}";
      }
    }

    [Display(Name = "Measure Min")]
    [DisplayFormat(DataFormatString = "{0:0.####}")]
    public decimal? MeasurementMin { get; set; }

    [Display(Name = "Measur Max")]
    [DisplayFormat(DataFormatString = "{0:0.####}")]
    public decimal? MeasurementMax { get; set; }

    [Display(Name = "Measure Unit")]
    public string MeasurementUnit { get; set; }

    public string Output
    {
      get
      {
        return $"{OutputMin:0.####} ... {OutputMax:0.####} {OutputUnit}";
      }
      set { }
    }

    [Display(Name = "OptMin")]
    public decimal? OutputMin { get; set; }

    [Display(Name = "OptMax")]
    public decimal? OutputMax { get; set; }

    [Display(Name = "Unit")]
    public string OutputUnit { get; set; }

    [Display(Name = "Material Nmb")]
    public string MaterialNumber { get; set; }

    public int DeviceClassId { get; set; }

    public string Description { get; set; }
  }
}
