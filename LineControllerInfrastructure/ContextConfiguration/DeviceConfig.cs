using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DeviceConfig : BaseModelConfig<Device>
  {
    public override void Configure(EntityTypeBuilder<Device> builder)
    {
      builder.ToTable(
           "Device",
           tb =>
           {
             tb.HasTrigger("UpdateItemNumber");
             tb.HasCheckConstraint(
                 "CHK_DeviceActivityTypeAndCostFactor",
                 "([ActivityTypeId] IS NULL) OR ([ActivityTypeId] IS NOT NULL AND [CostFactor] IS NOT NULL)");
           });

      builder.HasIndex(d => d.ItemNumber)
             .IsUnique();

      builder.Property(d => d.ItemNumber).ValueGeneratedOnAdd();
      builder.Property(d => d.ItemNumber).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
      builder.Property(d => d.CalibrationDate).HasConversion<DateTime>();
      builder.Property(d => d.CreationDate).HasConversion<DateTime>();
      builder.Property(d => d.MaterialNumber).HasColumnName("SAPOrderNumber");

      builder.HasOne(d => d.StoragePlace)
             .WithMany(sp => sp.Devices)
             .HasForeignKey(d => d.StoragePlaceId)
             .OnDelete(DeleteBehavior.NoAction);

      builder.HasOne(d => d.DeviceClass)
             .WithMany()
             .HasForeignKey(d => d.DeviceClassId)
             .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(d => d.Status)
             .WithMany()
             .HasForeignKey(d => d.StatusId).OnDelete(DeleteBehavior.Restrict);

      base.Configure(builder);

    }
  }
}
