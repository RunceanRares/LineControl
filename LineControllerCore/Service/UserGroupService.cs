using LineControllerCore.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;

namespace LineControllerCore.Service
{
  [ExcludeFromCodeCoverage]
  public class UserGroupService : IUserGroupService
  {
    private readonly UserGroupOptions options;

    public UserGroupService(IOptions<UserGroupOptions> options)
    {
      this.options = options.Value;
    }

    public bool IsMember(ClaimsPrincipal principal, string roleName)
    {
      string groupName = options.GroupPrefix + roleName;
      return principal.IsInRole(groupName);
    }
  }
}
