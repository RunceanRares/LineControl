using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class CalibrationActionConfig : IEntityTypeConfiguration<CalibrationAction>
  {
    public void Configure(EntityTypeBuilder<CalibrationAction> builder)
    {
      builder.ToTable("CalibrationAction");

      builder.HasData(
               new CalibrationAction { Id = 1, Name = "Calibration" },
               new CalibrationAction { Id = 2, Name = "Checking" },
               new CalibrationAction { Id = 3, Name = "DMS application" }
      );
    }
  }
}
