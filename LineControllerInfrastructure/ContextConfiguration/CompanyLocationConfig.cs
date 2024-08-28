using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class CompanyLocationConfig : BaseModelConfig<CompanyLocation>
  {
    public override void Configure(EntityTypeBuilder<CompanyLocation> builder)
    {
      builder.ToTable("CompanyLocation");

      builder.HasIndex(cl => cl.Code)
             .IsUnique();

      base.Configure(builder);
    }
  }
}
