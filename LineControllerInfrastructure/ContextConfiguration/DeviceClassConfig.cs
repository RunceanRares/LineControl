using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DeviceClassConfig : BaseModelConfig<DeviceClass>
  {
    public override void Configure(EntityTypeBuilder<DeviceClass> builder)
    {
      builder.ToTable("DeviceClass");

      builder.HasIndex(dc => new { dc.DeviceModelId })
             .IsUnique();

      builder.HasOne(dc => dc.DeviceModel)
             .WithMany()
             .HasForeignKey(dc => dc.DeviceModelId)
             .OnDelete(DeleteBehavior.Restrict);


      base.Configure(builder);
    }
  }
}
