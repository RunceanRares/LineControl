using LineControllerCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Service
{
  public class UserRoleService : IUserRoleService
  {
    private readonly IIdentityService identityService;
    private readonly IActiveDirectoryService activeDirectoryService;

    public UserRoleService(IIdentityService identityService, IActiveDirectoryService activeDirectoryService)
    {
      this.identityService = identityService;
      this.activeDirectoryService = activeDirectoryService;
    }

    public bool IsMember(IEnumerable<string> roleNames)
    {
      if (!identityService.IsAuthenticated)
      {
        return false;
      }

      return roleNames.Any(roleName => identityService.IsMember(roleName));
    }

    public bool IsMember(string userName, string roleName)
    {
      return activeDirectoryService.IsMember(userName, roleName);
    }
  }
}
