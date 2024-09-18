
using LineControllerCore.Models;

namespace LineControllerCore.Interface
{
  public interface IDeviceCalibrationService
  {
    IQueryable<DeviceCalibrationOrderViewModel> GetSelectViewModels();
  }
}
