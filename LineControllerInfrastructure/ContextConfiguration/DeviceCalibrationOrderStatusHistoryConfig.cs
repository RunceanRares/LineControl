using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DeviceCalibrationOrderStatusHistoryConfig : BaseModelConfig<DeviceCalibrationOrderStatusHistory>
  {
    public override void Configure(EntityTypeBuilder<DeviceCalibrationOrderStatusHistory> builder)
    {
      builder.ToTable("DeviceCalibrationOrderStatusHistory");

      builder.Property(cosh => cosh.LastChangedDate).IsRequired();

      builder.HasOne(cosh => cosh.Status)
             .WithMany()
             .HasForeignKey(cosh => cosh.StatusId).OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(cosh => cosh.CalibrationOrder)
             .WithMany(co => co.StatusHistory)
             .HasForeignKey(cosh => cosh.CalibrationOrderId).OnDelete(DeleteBehavior.Restrict);

      base.Configure(builder);
      builder.Property(cosh => cosh.LastChangedUserId).IsRequired();
    }
  }
}
