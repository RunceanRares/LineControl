using LineControllerCore.Model;

namespace LineControllerCore.Interface
{
  public interface IDeviceClassMode
  {
    IQueryable<DeviceClassModeViewModel> GetDeviceClassModeAsync();
    List<DeviceModelViewModel> GetDeviceMode();

    DeviceClassModeViewModel GetDeviceClassModelById(int id);
    DeviceModelViewModel GetDeviceModelById(int id);
    DeviceClassModeViewModel UpdateDeviceClassModel(DeviceClassModeViewModel model);
    DeviceModelViewModel UpdateDeviceModel(DeviceModelViewModel model);
  }
}
