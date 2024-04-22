using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Interface
{
  public interface IActiveDirectoryService
  {
    IEnumerable<string> GetUsers(string roleName);

    bool IsMember(string userName, string roleName);
  }
}
