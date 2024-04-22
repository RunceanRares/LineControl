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
  public class DeviceIssueConfig : IEntityTypeConfiguration<DeviceIssue>
  {
    public void Configure(EntityTypeBuilder<DeviceIssue> builder)
    {
      builder.ToTable("DeviceIssue");

      builder.HasOne(di => di.Collector)
             .WithMany()
             .HasForeignKey(di => di.CollectorId)
             .OnDelete(DeleteBehavior.NoAction);

      builder.HasOne(di => di.CreatedBy)
             .WithMany()
             .HasForeignKey(di => di.CreatedById)
             .OnDelete(DeleteBehavior.NoAction);

      builder.HasOne(di => di.Recipient)
             .WithMany()
             .HasForeignKey(di => di.RecipientId)
             .OnDelete(DeleteBehavior.NoAction);
    }
  }
}
