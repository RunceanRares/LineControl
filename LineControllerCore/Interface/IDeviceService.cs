using LineControl.Models;
using LineControllerCore.Model;

namespace LineControllerCore.Service
{
  public interface IDeviceService
  {
    //Task<IEnumerable<DeviceViewModel>> GetAsync(Func<LinkViewModel, string> getDeviceDetailsUrl, Func<LinkViewModel, string> getCalibrationOrderUrl);
    IQueryable<DeviceViewModel> GetDevices();

    DeviceEditViewModel GetDeviceById(int id);

    Task<IEnumerable<MeasurementRangeViewModel>> GetMeasurementRangesAsync(int deviceClassId);
  }
}