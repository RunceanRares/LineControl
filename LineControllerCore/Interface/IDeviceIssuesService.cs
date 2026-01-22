using LineControllerCore.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Interface
{
  public interface IDeviceIssuesService
  {
    Task<List<UserSelectViewModel>> GetActiveUsers();

    IEnumerable<object> SearchItemNumber(string itemNumber);

    Task<DeviceIssueStatusViewModel> GetDeviceIssuesStatus(string itemNumber);

    Task<DeviceIssueEditViewModel> IssueAsync(DeviceIssueEditViewModel deviceIssue);

    Task<DeviceIssueEditViewModel> ReceiveAsync(DeviceIssueEditViewModel deviceIssue);

    IQueryable<DeviceIssueHierarchyListViewModel> GetIssueHierarchy(int? deviceId);

    Task<DeviceIssueEditViewModel> GetViewModelByItemNumberAsync(string itemNumber);

  }
}
