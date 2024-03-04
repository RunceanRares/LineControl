using LineControllerCore.Interface;
using System.Globalization;
using System.Security.Claims;
namespace LineControl.Common
{
  public class IdentityService : IIdentityService
  {

    private readonly IHttpContextAccessor httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
      this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
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
      throw new NotImplementedException();
    }
  }
}
