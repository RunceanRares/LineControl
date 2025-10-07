using LineControllerCore.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Interface
{
  public interface IDeviceIntegrationService
  {
    IQueryable<DeviceHierarchyListViewModel> GetHierarchy(int id);
  }
}
