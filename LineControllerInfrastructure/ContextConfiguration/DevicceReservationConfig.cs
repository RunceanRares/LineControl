using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DevicceReservationConfig : IEntityTypeConfiguration<DeviceReservation>
  {
    public void Configure(EntityTypeBuilder<DeviceReservation> builder)
    {
      builder.ToTable("DeviceReservation");

      builder.HasOne(dr => dr.Device)
             .WithMany() 
             .HasForeignKey(dr => dr.DeviceId)
             .OnDelete(DeleteBehavior.NoAction);

      builder.HasOne(dr => dr.User)
             .WithMany()
             .HasForeignKey(dr => dr.UserId)
             .OnDelete(DeleteBehavior.NoAction);
    }
  }
}
