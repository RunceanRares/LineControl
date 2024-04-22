using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Interface
{
  public interface IUserGroupService
  {
    bool IsMember(ClaimsPrincipal principal, string roleName);
  }
}
