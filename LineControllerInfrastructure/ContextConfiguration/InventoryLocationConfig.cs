using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class InventoryLocationConfig : BaseModelConfig<InventoryLocation>
  {
    public override void Configure(EntityTypeBuilder<InventoryLocation> builder)
    {
      builder.ToTable(
          "InventoryLocation",
          t => t.HasCheckConstraint(
             "CHK_ActivityTypeAndCostFactor",
             "([ActivityTypeId] IS NULL AND [CostFactor] IS NULL) OR ([ActivityTypeId] IS NOT NULL AND [CostFactor] IS NOT NULL)"));

      builder.HasOne(il => il.Responsible)
             .WithMany()
             .HasForeignKey(il => il.ResponsibleId).OnDelete(DeleteBehavior.Restrict);

      builder.Property(m => m.LastChangedDate).HasConversion<DateTime>();

      builder.HasOne(m => m.LastChangedUser)
             .WithMany()
             .HasForeignKey(m => m.LastChangedUserId)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.Restrict);

      base.Configure(builder);
    }
  }
}
