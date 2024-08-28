using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DeviceCalibrationOrderStatusConfig : IEntityTypeConfiguration<DeviceCalibrationOrderStatus>
  {
    public void Configure(EntityTypeBuilder<DeviceCalibrationOrderStatus> builder)
    {
      builder.ToTable("DeviceCalibrationOrderStatus");

      builder.Property(s => s.Name)
             .IsRequired();

      builder.HasData(
             new DeviceCalibrationOrderStatus { Id = DeviceCalibrationOrderStatus.Received, Name = "Received" },
             new DeviceCalibrationOrderStatus { Id = DeviceCalibrationOrderStatus.InProgress, Name = "In Progress" },
             new DeviceCalibrationOrderStatus { Id = DeviceCalibrationOrderStatus.SentExternally, Name = "Sent Externally" },
             new DeviceCalibrationOrderStatus { Id = DeviceCalibrationOrderStatus.FinishedOK, Name = "Finished (OK)" },
             new DeviceCalibrationOrderStatus { Id = DeviceCalibrationOrderStatus.FinishedNOK, Name = "Finished (NOK)" },
             new DeviceCalibrationOrderStatus { Id = DeviceCalibrationOrderStatus.FinishedAdjusted, Name = "Finished (Adjusted)" },
             new DeviceCalibrationOrderStatus { Id = DeviceCalibrationOrderStatus.FinishedOKAdjusted, Name = "Finished (OK after Adjustment)" },
             new DeviceCalibrationOrderStatus { Id = DeviceCalibrationOrderStatus.FinishedScrap, Name = "Finished (Scrap)" },
             new DeviceCalibrationOrderStatus { Id = DeviceCalibrationOrderStatus.Returned, Name = "Returned" }
         );
    }
  }
}
