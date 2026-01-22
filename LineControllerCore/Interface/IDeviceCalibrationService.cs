
using LineControl.Models;

using LineControllerCore.Model;
using LineControllerCore.Models;

namespace LineControllerCore.Interface
{
  public interface IDeviceCalibrationService
  {
    IQueryable<DeviceCalibrationOrderViewModel> GetSelectViewModels();

    DeviceCalibrationOrderViewModel GetDeviceCalibrationById(int id);

    DeviceCalibrationOrderViewModel Update(DeviceCalibrationOrderViewModel model);

    DeviceCalibrationOrderViewModel AddCalibrationOrder(DeviceCalibrationOrderViewModel model);

    Task<IEnumerable<DeviceViewModel>> GetItemNumbers(string itemNumber);

    Task<IEnumerable<CalibrationLocationViewModel>> GetLocationAsync();

    Task<List<CalibrationOrderActionViewModel>> GetCalibrationAction();

    Task<List<UserSelectViewModel>> GetUserCalibration();

    Task<DeviceViewModel> GetDeviceCreator(int deviceId);

    Task<DeviceViewModel> GetDeviceTestLocation(int deviceId);

  }
}
