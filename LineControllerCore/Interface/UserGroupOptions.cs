using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Interface
{
  [ExcludeFromCodeCoverage]
  public class UserGroupOptions
  {
    public virtual string DomainName { get; set; }

    public virtual string GroupPrefix { get; set; }
  }
}
