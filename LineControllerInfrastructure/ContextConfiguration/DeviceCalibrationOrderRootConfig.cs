using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DeviceCalibrationOrderRootConfig : BaseModelConfig<DeviceCalibrationOrderRoot>
  {
    public override void Configure(EntityTypeBuilder<DeviceCalibrationOrderRoot> builder)
    {
      builder.ToTable("DeviceCalibrationOrderRoot"); 

      builder.Property(co => co.AccountingType).HasConversion<int>();

      builder.HasOne(dcor => dcor.Receiver)
             .WithMany()
             .HasForeignKey(dcor => dcor.ReceiverId)
             .OnDelete(DeleteBehavior.NoAction);

      base.Configure(builder);
    }
  }
}
