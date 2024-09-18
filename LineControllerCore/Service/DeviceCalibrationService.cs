using AutoMapper;
using AutoMapper.QueryableExtensions;
using LineControllerCore.Interface;
using LineControllerCore.Models;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LineControllerCore.Service
{
  public class DeviceCalibrationService : BaseService<DeviceCalibrationOrder>, IDeviceCalibrationService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeviceCalibrationService(LineContextDb context, IMapper mapper, ILogger<DeviceCalibrationService> logger, IHttpContextAccessor httpContextAccessor)
          : base(context, mapper, logger)
    {
      this._httpContextAccessor = httpContextAccessor;
    }

    public IQueryable<DeviceCalibrationOrderViewModel> GetSelectViewModels()
    {
      return Context.CalibrationOrders.ProjectTo<DeviceCalibrationOrderViewModel>(Mapper.ConfigurationProvider);
    }
  }
}
