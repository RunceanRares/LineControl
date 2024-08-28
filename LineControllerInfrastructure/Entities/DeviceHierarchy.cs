using System.Diagnostics.CodeAnalysis;

namespace LineControllerInfrastructure.Entities
{
  public class DeviceHierarchy
  {
    [ExcludeFromCodeCoverage]
    public int ParentId { get; set; }

    public int ChildId { get; set; }

    public int Depth { get; set; }
  }
}
