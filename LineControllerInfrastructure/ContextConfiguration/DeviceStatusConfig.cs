using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DeviceStatusConfig : IEntityTypeConfiguration<DeviceStatus>
  {
    public void Configure(EntityTypeBuilder<DeviceStatus> builder)
    {
      builder.ToTable("DeviceStatus");

      builder.HasData(
           new DeviceStatus() { Id = DeviceStatus.UsableId, Name = "usable" },
           new DeviceStatus() { Id = DeviceStatus.DefectId, Name = "defect" },
           new DeviceStatus() { Id = DeviceStatus.DisposedId, Name = "given away/disposed" },
           new DeviceStatus() { Id = DeviceStatus.LostId, Name = "lost" },
           new DeviceStatus() { Id = DeviceStatus.LockedId, Name = "locked" },
           new DeviceStatus() { Id = DeviceStatus.InServiceId, Name = "being serviced" });
    }
  }
}
