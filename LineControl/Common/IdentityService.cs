using LineControllerCore.Interface;
using LineControllerCore.Service;

using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using System.Globalization;
using System.Security.Claims;

namespace LineControl.Common
{
  public class IdentityService : IIdentityService
  {
    private readonly IUserGroupService userGroupService;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly LineContextDb context;

    public IdentityService(IHttpContextAccessor httpContextAccessor, IUserGroupService userGroupService, LineContextDb context)
    {
      this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
      this.userGroupService = userGroupService;
      this.context = context;
    }

    private ClaimsPrincipal User => httpContextAccessor.HttpContext?.User;

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    public int? UserId
    {
      get
      {
        var windowsName = httpContextAccessor.HttpContext?.User?.Identity?.Name;
        string userName = windowsName.Split('\\')[0];

        if (string.IsNullOrEmpty(userName)) return null;

        var user = context.Users.FirstOrDefault(u => u.UserName == userName);

        return user?.Id;
      }
    }

    public bool IsMember(string roleName)
    {
      try
      {
        string sessionKey = "Line" + roleName;
        string? sessionValue = httpContextAccessor.HttpContext!.Session.GetString(sessionKey);
        if (string.IsNullOrEmpty(sessionValue))
        {
          bool isMember = userGroupService.IsMember(User, roleName);
          if (isMember)
          {
            httpContextAccessor.HttpContext.Session.SetString(sessionKey, bool.TrueString);
          }
          else
          {
            httpContextAccessor.HttpContext.Session.SetString(sessionKey, bool.FalseString);
          }

          return isMember;
        }

        return string.Equals(sessionValue, bool.TrueString, StringComparison.InvariantCultureIgnoreCase);
      }
      catch (InvalidOperationException)
      {
        return userGroupService.IsMember(User, roleName);
      }
    }
  }
}
