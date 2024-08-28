using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public abstract class BaseModelConfig<TEntity> : IEntityTypeConfiguration<TEntity>
       where TEntity : BaseModel
  {
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
      builder.Property(m => m.LastChangedDate).HasConversion<DateTime>();

      builder.HasOne(m => m.LastChangedUser)
         .WithMany()
         .HasForeignKey(m => m.LastChangedUserId)
         .IsRequired(false)
         .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
