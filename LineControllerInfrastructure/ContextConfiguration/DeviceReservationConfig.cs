using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DeviceReservationConfig : BaseModelConfig<DeviceReservation>
  {
    public override void Configure(EntityTypeBuilder<DeviceReservation> builder)
    {
      builder.ToTable("DeviceReservation");

      builder.Property(r => r.StartDate).HasConversion<DateTime>();
      builder.Property(d => d.CreationDate).HasConversion<DateTime>();

      builder.HasOne(r => r.User)
             .WithMany()
             .HasForeignKey(r => r.UserId)
             .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(r => r.Issue)
             .WithOne(i => i.Reservation)
             .HasForeignKey<DeviceReservation>(r => r.IssueId).OnDelete(DeleteBehavior.Restrict);

      builder.HasOne<DeviceClass>()
             .WithMany()
             .HasForeignKey(r => r.DeviceClassId)
             .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(r => r.Device)
             .WithMany(d => d.Reservations)
             .HasForeignKey(r => r.DeviceId)
             .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(r => r.InventoryLocation)
             .WithMany()
             .HasForeignKey(r => r.InventoryLocationId)
             .IsRequired().OnDelete(DeleteBehavior.NoAction);

      builder.HasOne(m => m.CreatedBy)
             .WithMany()
             .HasForeignKey(m => m.CreatedById)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

      base.Configure(builder);

    }
  }
}
