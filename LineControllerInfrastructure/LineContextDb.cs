using LineControllerInfrastructure.ContextConfiguration;
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

    public DbSet<CompanyLocation> CompanyLocations { get; set; } = null;

    public DbSet<ActivityType> ActivityTypes { get; set; }

    public DbSet<DeviceClass> DeviceClasses { get; set; }

    public DbSet<DeviceHistory> DeviceHistories { get; set; }

    public DbSet<DeviceIssue> DeviceIssues { get; set; }

    public DbSet<DeviceModel> DeviceModels { get; set; }

    public DbSet<DeviceReservation> DeviceReservations { get; set; }

    public DbSet<DeviceStatus> DeviceStatuses { get; set; }

    public DbSet<InventoryLocation> InventoryLocations { get; set; }

    public DbSet<ReservationPeriod> ReservationPeriods { get; set; }

    public DbSet<ReservationStatus> ReservationStatuses { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<StoragePlace> StoragePlaces { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfiguration(new DeviceHistoryConfig());
      modelBuilder.ApplyConfiguration(new StoragePlaceConfig());
      modelBuilder.ApplyConfiguration(new DeviceIssueConfig());
      modelBuilder.ApplyConfiguration(new DevicceReservationConfig());
    }

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
