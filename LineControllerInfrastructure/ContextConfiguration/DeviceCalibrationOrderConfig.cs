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

      builder.HasOne(d => d.Root)
             .WithMany()
             .HasForeignKey(co => co.RootId).OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(dco => dco.Device)
             .WithMany()
             .HasForeignKey(dco => dco.DeviceId)
             .OnDelete(DeleteBehavior.Restrict);


      base.Configure(builder);
    }
  }
}
