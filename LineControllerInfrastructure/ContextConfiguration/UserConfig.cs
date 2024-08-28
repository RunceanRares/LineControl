using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LineControllerInfrastructure.ContextConfiguration
{
  public class UserConfig : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.ToTable(
         "User",
         t => t.HasCheckConstraint(
            "CHK_EmailAndDepartment",
            "[Status] <> 1 OR ([Email] IS NOT NULL AND [Email] <> '' AND [Department] IS NOT NULL AND [Department] <> '')"));
      builder.HasIndex(u => u.PersonalNumber)
             .IsUnique();

      builder.HasIndex(u => u.UserName)
             .IsUnique();

      builder.HasOne(u => u.CompanyLocation)
             .WithMany(cl => cl.Users)
             .HasForeignKey(u => u.CompanyLocationId)
             .IsRequired(false) 
             .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(u => u.Manager)
             .WithMany()
             .HasForeignKey(u => u.ManagerId)
             .IsRequired(false).OnDelete(DeleteBehavior.Restrict);
    }
  }
}
