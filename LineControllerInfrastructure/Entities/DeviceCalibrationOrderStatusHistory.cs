using System.ComponentModel.DataAnnotations.Schema;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceCalibrationOrderStatusHistory : BaseModel
  {
    public DeviceCalibrationOrder? CalibrationOrder { get; set; }

    [ForeignKey(nameof(CalibrationOrder))]
    public int CalibrationOrderId { get; set; }

    public DeviceCalibrationOrderStatus? Status { get; set; }

    [ForeignKey(nameof(Status))]
    public int StatusId { get; set; }
  }
}
