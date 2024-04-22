using LineControllerCore.Interface;
using System.DirectoryServices.AccountManagement;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Service
{
  public class ActiveDirectoryService : IActiveDirectoryService
  {
    private readonly UserGroupOptions options;

    public ActiveDirectoryService(IOptions<UserGroupOptions> options)
    {
      this.options = options.Value;
    }

    public IEnumerable<string> GetUsers(string roleName)
    {
      string groupName = options.GroupPrefix + roleName;

      using var context = new PrincipalContext(ContextType.Domain, options.DomainName);
      using var group = GroupPrincipal.FindByIdentity(context, groupName) ?? throw new InvalidOperationException($"The group {groupName} does not exist");

      List<string> names = new List<string>();
      var users = group.GetMembers(true);
      foreach (var user in users)
      {
        names.Add(user.SamAccountName);
      }

      return names;
    }

    public bool IsMember(string userName, string roleName)
    {
      string groupName = options.GroupPrefix + roleName;

      using var context = new PrincipalContext(ContextType.Domain, options.DomainName);
      using var group = GroupPrincipal.FindByIdentity(context, groupName) ?? throw new InvalidOperationException($"The group {groupName} does not exist");
      using var user = UserPrincipal.FindByIdentity(context, userName) ?? throw new InvalidOperationException($"The user {userName} does not exist");

      return user.IsMemberOf(group);
    }
  }
}
