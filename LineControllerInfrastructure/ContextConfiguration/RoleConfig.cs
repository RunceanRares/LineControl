using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class RoleConfig : IEntityTypeConfiguration<Role>
  {
    public void Configure(EntityTypeBuilder<Role> builder)
    {
      builder.ToTable("Role");

      builder.HasIndex(r => r.Name)
             .IsUnique();

      builder.HasData(
             new Role()
             {
               Id = 1,
               Name = "SysAdmin",
               Deactivated = false,
             },
             new Role()
             {
               Id = 2,
               Name = "CalibrationStaff",
               Deactivated = false,
             },
             new Role()
             {
               Id = 3,
               Name = "DeviceManager",
               Deactivated = false,
             },
             new Role()
             {
               Id = 4,
               Name = "User",
               Deactivated = false,
             },
             new Role()
             {
               Id = 5,
               Name = "DeviceMaster",
               Deactivated = false,
             },
             new Role()
             {
               Id = 6,
               Name = "ChargeAdmin",
               Deactivated = false,
             },
             new Role()
             {
               Id = 7,
               Name = "API",
               Deactivated = false,
             });
    }
  }
}
