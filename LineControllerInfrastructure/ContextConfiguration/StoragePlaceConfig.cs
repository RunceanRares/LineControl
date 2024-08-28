using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class StoragePlaceConfig : BaseModelConfig<StoragePlace>
  {
    public override void Configure(EntityTypeBuilder<StoragePlace> builder)
    {
      builder.ToTable(
            "StoragePlace",
            t => t.HasCheckConstraint(
               "CK_StoragePlace_Building_RoomDesignation",
               "ISNULL([Building], '') + ISNULL([RoomDesignation], '') > ''"));

      builder.HasIndex(sp => sp.InventoryLocationId);

      builder.HasIndex(sp => new { sp.InventoryLocationId, sp.CompanyLocationId, sp.Building, sp.Floor, sp.RoomNumber, sp.RoomDesignation, sp.Place })
            .IsUnique();

      builder.HasIndex(sp => new { sp.InventoryLocationId, sp.Default })
             .IsUnique()
             .HasFilter("[Default] = 1");

      builder.HasOne(sp => sp.InventoryLocation)
             .WithMany(il => il.StoragePlaces)
             .HasForeignKey(sp => sp.InventoryLocationId).OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(sp => sp.CompanyLocation)
             .WithMany()
             .HasForeignKey(sp => sp.CompanyLocationId).OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(sp => sp.Responsible)
             .WithMany()
             .HasForeignKey(sp => sp.ResponsibleId)
             .OnDelete(DeleteBehavior.Restrict).OnDelete(DeleteBehavior.Restrict);

      base.Configure(builder);
    }
  }
}
