using LineControllerCore.Interface;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace LineControllerCore.Service
{
  public class UserRoleService : IUserRoleService
  {
    private readonly IIdentityService identityService;
    private readonly IActiveDirectoryService activeDirectoryService;
    private readonly LineContextDb context;

    public UserRoleService(LineContextDb contextDb, IIdentityService identityService, IActiveDirectoryService activeDirectoryService)
    {
      this.context = contextDb;
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

    public async Task<bool> HasRightsAsync(string userAction)
    {
      if(!identityService.IsAuthenticated)
      {
        return false;
      }

      //store the parse value in variable action 
      if (!Enum.TryParse<UserRoles>(userAction, out var action))
      {
        return false;
      }

      var roles = await context.Users.Where(r => r.UserRoles.HasFlag(action) && r.Status == 1).ToListAsync().ConfigureAwait(false);
      var userRoleNames = roles.SelectMany(u => Enum.GetNames(typeof(UserRoles)).Where(r => (u.UserRoles & (UserRoles)Enum.Parse(typeof(UserRoles), r)) != 0));

      foreach (var roleName in userRoleNames)
      {
        if (identityService.IsMember(roleName))
        {
          return true;
        }
      }

      return false;
    }

    public bool IsMember(string userName, string roleName)
    {
      return activeDirectoryService.IsMember(userName, roleName);
    }
  }
}
