using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class ReservationStatusConfig : IEntityTypeConfiguration<ReservationStatus>
  {
    public void Configure(EntityTypeBuilder<ReservationStatus> builder)
    {
      builder.ToTable("ReservationStatus");

      builder.HasData(
           new ReservationStatus() { Id = 1, Name = "Open" },
           new ReservationStatus() { Id = 2, Name = "Canceled, in accordance with deadline" },
           new ReservationStatus() { Id = 3, Name = "Canceled, not in accordance with deadline" },
           new ReservationStatus() { Id = 4, Name = "Not collected" },
           new ReservationStatus() { Id = 5, Name = "Collected" },
           new ReservationStatus() { Id = 6, Name = "Alternative rejected or not present" });
    }
  }
}