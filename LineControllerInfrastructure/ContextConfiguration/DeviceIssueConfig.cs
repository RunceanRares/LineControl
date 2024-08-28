using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class DeviceIssueConfig : BaseModelConfig<DeviceIssue>
  {
    public override void Configure(EntityTypeBuilder<DeviceIssue> builder)
    {
      builder.ToTable("DeviceIssue");

      builder.Property(i => i.AccountingType).HasConversion<int?>();
      builder.Property(i => i.IssueDate).HasConversion<DateTime>();
      builder.Property(i => i.ReturnDatePlanned).HasConversion<DateTime>();
      builder.Property(i => i.ReturnDateActual).HasConversion<DateTime>();
      builder.Property<int>("AvoidDuplicate").HasComputedColumnSql("CASE WHEN [ReturnDateActual] IS NULL THEN [DeviceId] ELSE -[DeviceIssueId] END");
      builder.HasIndex(i => i.ReturnDateActual).IsClustered(false);

      builder.HasOne(i => i.Device)
         .WithMany(d => d.Issues)
         .HasForeignKey(i => i.DeviceId)
         .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(i => i.Recipient)
         .WithMany()
         .HasForeignKey(i => i.RecipientId)
         .OnDelete(DeleteBehavior.Restrict);

      base.Configure(builder);
    }
  }
}
