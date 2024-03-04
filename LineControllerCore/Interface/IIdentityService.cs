using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Interface
{
  public interface IIdentityService
  {
    int? UserId { get; }

    bool IsAuthenticated { get; }

    bool IsMember(string roleName);
  }
}
