using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class ActivityTypeConfig : BaseModelConfig<ActivityType>
  {
    public override void Configure(EntityTypeBuilder<ActivityType> builder)
    {
      builder.ToTable("ActivityType");

      builder.HasIndex(at => at.Name)
             .IsUnique();

      base.Configure(builder);
    }
  }
}
