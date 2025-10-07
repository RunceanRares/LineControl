
using LineControl.Models;

using LineControllerCore.Models;

namespace LineControllerCore.Interface
{
  public interface IDeviceCalibrationService
  {
    IQueryable<DeviceCalibrationOrderViewModel> GetSelectViewModels();

    DeviceCalibrationOrderViewModel GetDeviceCalibrationById(int id);

    DeviceCalibrationOrderViewModel Update(DeviceCalibrationOrderViewModel model);

    DeviceCalibrationOrderViewModel AddCalibratioOrder(DeviceCalibrationOrderViewModel model);

    Task<IEnumerable<DeviceViewModel>> GetItemNumbers(string itemNumber);
  }
}
