
using LineControllerInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LineControllerInfrastructure
{
  public class LineContextDb : DbContext
  {
    public LineContextDb(DbContextOptions<LineContextDb> options) : base(options)
    { 
    }

    public DbSet<User> Users { get; set; } = null;

    public override int SaveChanges()
    {
      HandleSoftDelete();
      return base.SaveChanges();
    }

    private void HandleSoftDelete()
    {
      var entities = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
      foreach (var entity in entities)
      {

        var deleteEntity = entity.Entity as BaseModel;
        if (deleteEntity is not null)
        {
          entity.State = EntityState.Modified;
          deleteEntity.IsDeleted = true;
        }
      }
    }
  }
}
