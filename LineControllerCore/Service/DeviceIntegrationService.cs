using AutoMapper;
using AutoMapper.QueryableExtensions;
using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LineControllerCore.Service
{
  public class DeviceIntegrationService : BaseService<Device>, IDeviceIntegrationService
  {
    public DeviceIntegrationService(LineContextDb context, IMapper mapper, ILogger<DeviceIntegrationService> logger) : base(context, mapper, logger)
    {
    }

    public IQueryable<DeviceHierarchyListViewModel> GetHierarchy(int id)
    {
      DateTime now = DateTime.Now;
      var parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
      {
         { "now", now },
      };
      return Context.GetDeviceChildrenTree(id).ProjectTo<DeviceHierarchyListViewModel>(Mapper.ConfigurationProvider, parameters);
    }
  }
}
