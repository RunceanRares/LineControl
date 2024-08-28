using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DeviceClassModeConfig : BaseModelConfig<DeviceClassMode>
  {
    public override void Configure(EntityTypeBuilder<DeviceClassMode> builder)
    {
      builder.ToTable("DeviceClassMode");
      builder.Property(dcm => dcm.Description);
      base.Configure(builder);
    }
  }
}
