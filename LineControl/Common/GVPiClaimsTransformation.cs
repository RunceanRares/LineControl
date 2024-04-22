using LineControllerCore.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace LineControl.Common
{
  public class GVPiClaimsTransformation : IClaimsTransformation
  {
    private readonly IUserService userService;
    private readonly IMemoryCache memoryCache;
    private readonly IUserGroupService userGroupService;

    public GVPiClaimsTransformation(IUserService userService, IUserGroupService userGroupService, IMemoryCache memoryCache)
    {
      this.userService = userService;
      this.userGroupService = userGroupService;
      this.memoryCache = memoryCache;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
      try
      {
        if (!((ClaimsIdentity)principal.Identity)!.HasClaim("HasCustomClaims", "true"))
        {
          var userName = principal.FindFirstValue(ClaimTypes.NameIdentifier);
          var id = (ClaimsIdentity)principal.Identity;

          int? userId = await getUserId(userName).ConfigureAwait(false);
        }

        return principal;
      }
      catch(Exception ex)
      {
        throw;
      }
    }

    private async Task<int?> getUserId(string userName)
    {
      string key = "Id_" + userName;
      int? userId;
      if (!memoryCache.TryGetValue(key, out int id))
      {
        userId = await userService.GetUserIdAsync(userName).ConfigureAwait(false);
        if (userId != null)
        {
          memoryCache.Set(key, userId.Value, new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = new TimeSpan(0, 20, 0) });
        }
      }
      else
      {
        userId = id;
      }

      return userId;
    }
  }
}
