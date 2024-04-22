using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class StoragePlaceConfig : IEntityTypeConfiguration<StoragePlace>
  {
    public void Configure(EntityTypeBuilder<StoragePlace> builder)
    {
      builder.ToTable("StoragePlace");

      builder.HasOne(sp => sp.InventoryLocation)
             .WithMany(il => il.StoragePlaces)
             .HasForeignKey(sp => sp.InventoryLocationId)
             .OnDelete(DeleteBehavior.NoAction);

      builder.HasOne(sp => sp.Responsible)
             .WithMany()
             .HasForeignKey(sp => sp.ResponsibleId)
             .OnDelete(DeleteBehavior.NoAction);
    }
  }
}
