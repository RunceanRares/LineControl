using LineControllerCore.Interface;
using System.Globalization;
using System.Security.Claims;
namespace LineControl.Common
{
  public class IdentityService : IIdentityService
  {
    private readonly IUserGroupService userGroupService;
    private readonly IHttpContextAccessor httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor, IUserGroupService userGroupService)
    {
      this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
      this.userGroupService = userGroupService;
    }

    private ClaimsPrincipal User => httpContextAccessor.HttpContext!.User;

    public bool IsAuthenticated
    {
      get
      {
        return User.Identity!.IsAuthenticated && User.FindFirst("USERID") != null;
      }
    }

    public int? UserId
    {
      get
      {
        Claim? claim = User.FindFirst("USERID");

        if (claim == null)
        {
          return null;
        }

        return int.Parse(claim.Value, CultureInfo.InvariantCulture);
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
