using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DeviceModelConfig : BaseModelConfig<DeviceModel>
  {
    public override void Configure(EntityTypeBuilder<DeviceModel> builder)
    {
      builder.ToTable("DeviceModel");

      builder.HasIndex(m => m.Name)
             .IsUnique();

      base.Configure(builder);
    }
  }
}
