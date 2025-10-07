using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DeviceCalibrationOrderConfig : BaseModelConfig<DeviceCalibrationOrder>
  {
    public override void Configure(EntityTypeBuilder<DeviceCalibrationOrder> builder)
    {
      builder.ToTable("DeviceCalibrationOrder");

      builder.Property(co => co.CalibrationDate).HasConversion<DateTime>();

      builder.HasOne(co => co.Device)
             .WithMany(d => d.CalibrationOrders)
             .HasForeignKey(co => co.DeviceId);

      builder.HasOne<DeviceStatus>()
             .WithMany()
             .HasForeignKey(co => co.PreviousDeviceStatusId)
             .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(o => o.Root)
       .WithMany()
       .HasForeignKey(o => o.RootId)
       .OnDelete(DeleteBehavior.NoAction);

      base.Configure(builder);
    }
  }
}
