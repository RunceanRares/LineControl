using System.Diagnostics.CodeAnalysis;

namespace LineControllerCore.Interface
{
  [ExcludeFromCodeCoverage]
  public class UserGroupOptions
  {
    public virtual string DomainName { get; set; }

    public virtual string GroupPrefix { get; set; }
  }
}
