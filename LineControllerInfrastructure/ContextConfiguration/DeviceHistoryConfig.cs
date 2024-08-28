using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DeviceHistoryConfig : IEntityTypeConfiguration<DeviceHistory>
  {
    public void Configure(EntityTypeBuilder<DeviceHistory> builder)
    {
      builder.ToTable("DeviceHistory");

      builder.HasKey(dh => new { dh.DeviceId, dh.ModificationUserId, dh.ModificationDate });

      builder.HasOne(dh => dh.ModificationUser)
             .WithMany()
             .HasForeignKey(dh => dh.ModificationUserId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
