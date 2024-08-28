using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class ReservationPeriodConfig : IEntityTypeConfiguration<ReservationPeriod>
  {
    public void Configure(EntityTypeBuilder<ReservationPeriod> builder)
    {
      builder.ToTable("ReservationPeriod");

      builder.Property(rp => rp.Name).IsRequired();

      builder.HasData(
         new ReservationPeriod { Id = 1, Min = 1, Max = 2, Name = "1 ... 2 days" },
         new ReservationPeriod { Id = 2, Min = 2, Max = 4, Name = "2 ... 4 days" },
         new ReservationPeriod { Id = 3, Min = 7, Max = 14, Name = "1 ... 2 weeks" },
         new ReservationPeriod { Id = 4, Min = 14, Max = 28, Name = "2 ... 4 weeks" },
         new ReservationPeriod { Id = 5, Min = 21, Max = 42, Name = "3 ... 6 weeks" },
         new ReservationPeriod { Id = 6, Min = 28, Max = 56, Name = "4 ... 8 weeks" },
         new ReservationPeriod { Id = 7, Min = 42, Max = 84, Name = "6 ... 12 weeks" },
         new ReservationPeriod { Id = 8, Min = 56, Max = 112, Name = "8 ... 16 weeks" },
         new ReservationPeriod { Id = 9, Min = 84, Max = 168, Name = "12 ... 24 weeks" },
         new ReservationPeriod { Id = 10, Min = 168, Max = null, Name = "more than 24" }
     );
    }
  }
}
