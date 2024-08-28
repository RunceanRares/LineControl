using AutoMapper;
using LineControllerCore.Interface;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using Microsoft.Extensions.Logging;

namespace LineControllerCore.Service
{
  public class CalibrationDeviceService : BaseService<DeviceCalibrationOrder>, ICalibrationDevice
  {
    public CalibrationDeviceService(LineContextDb context, IMapper mapper, ILogger<CalibrationDeviceService> logger) : base(context, mapper, logger)
    {
    }
  }
}
