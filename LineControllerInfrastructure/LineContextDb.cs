using LineControllerCore.Model;

using LineControllerInfrastructure.ContextConfiguration;
using LineControllerInfrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LineControllerInfrastructure
{
  public class LineContextDb : DbContext
  {
    public LineContextDb(DbContextOptions<LineContextDb> options) : base(options)
    { 
    }

    public DbSet<User> Users { get; set; }

    public DbSet<CompanyLocation> CompanyLocations { get; set; } 

    public DbSet<ActivityType> ActivityTypes { get; set; }

    public DbSet<DeviceHistory> DeviceHistories { get; set; }

    public DbSet<DeviceClassMode> DeviceClassModes { get; set; }

    public DbSet<DeviceIssue> DeviceIssues { get; set; }

    public DbSet<DeviceModel> DeviceModels { get; set; }

    public DbSet<DeviceReservation> DeviceReservations { get; set; }

    public DbSet<DeviceStatus> DeviceStatuses { get; set; }

    public DbSet<InventoryLocation> InventoryLocations { get; set; }

    public DbSet<ReservationPeriod> ReservationPeriods { get; set; }

    public DbSet<ReservationStatus> ReservationStatuses { get; set; }

    public DbSet<DeviceHierarchy> DeviceHierarchy { get; set; }

    public DbSet<Device> Devices { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<StoragePlace> StoragePlaces { get; set; }

    public DbSet<DeviceCalibrationOrder> CalibrationOrders { get; set; }

    public DbSet<DeviceCalibrationOrderStatus> CalibrationStatuses { get; set; }

    public DbSet<CalibrationAction> CalibrationActions { get; set; }

    public DbSet<CalibrationLocation> CalibrationLocations { get; set; }

    public DbSet<DeviceCalibrationOrderRoot> DeviceCalibrationOrderRoots { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfiguration(new DeviceHistoryConfig());
      modelBuilder.ApplyConfiguration(new StoragePlaceConfig());
      modelBuilder.ApplyConfiguration(new DeviceIssueConfig());
      modelBuilder.ApplyConfiguration(new DeviceReservationConfig());
      modelBuilder.ApplyConfiguration(new CompanyLocationConfig());
      modelBuilder.ApplyConfiguration(new DeviceConfig());
      modelBuilder.ApplyConfiguration(new DeviceCalibrationOrderRootConfig());
      modelBuilder.ApplyConfiguration(new DeviceCalibrationOrderStatusConfig());
      modelBuilder.ApplyConfiguration(new UserConfig());
      modelBuilder.ApplyConfiguration(new ReservationPeriodConfig());
      modelBuilder.ApplyConfiguration(new DeviceCalibrationOrderConfig());
      modelBuilder.ApplyConfiguration(new CalibrationActionConfig());
      modelBuilder.ApplyConfiguration(new InventoryLocationConfig());
      modelBuilder.ApplyConfiguration(new DeviceClassModeConfig());
      modelBuilder.ApplyConfiguration(new DeviceCalibrationOrderStatusHistoryConfig());
      modelBuilder.ApplyConfiguration(new ActivityTypeConfig());
      modelBuilder.ApplyConfiguration(new DeviceStatusConfig());
      modelBuilder.ApplyConfiguration(new ReservationStatusConfig());
      modelBuilder.ApplyConfiguration(new DeviceModelConfig());
      modelBuilder.ApplyConfiguration(new RoleConfig());

      modelBuilder.Entity<DeviceHierarchy>().ToView("DeviceHierarchy").HasKey(h => new { h.ParentId, h.ChildId });
    }

    public override int SaveChanges()
    {
      return base.SaveChanges();
    }

    public IQueryable<DeviceCalibrationOrder> ActiveCalibrationOrders
    {
      get
      {
        return CalibrationOrders.Where(co => co.StatusHistory.OrderByDescending(sh => sh.LastChangedDate)
                                                             .Select(sh => sh.StatusId)
                                                             .FirstOrDefault() < DeviceCalibrationOrderStatus.Active);
      }
    }

    private IQueryable<DeviceHierarchy> StructureRoot
    {
      get
      {
        return DeviceHierarchy.GroupBy(h => h.ChildId)
                              .Select(h => new { Id = h.Key, Depth = h.Max(d => d.Depth) })
                              .Join(DeviceHierarchy, h => new { Key1 = h.Id, Key2 = h.Depth }, h => new { Key1 = h.ChildId, Key2 = h.Depth }, (_, h) => h);
      }
    }

    public IQueryable<Device> GetDeviceTree(int deviceId)
    {
      return StructureRoot.Where(m => m.ChildId == deviceId)
                          .Join(DeviceHierarchy, h => h.ParentId, h => h.ParentId, (_, h2) => h2)
                          .Join(Devices, h => h.ChildId, d => d.Id, (_, d) => d);
    }

    public IQueryable<Device> GetDeviceTree(IEnumerable<int> deviceIds)
    {
      return StructureRoot.Where(m => deviceIds.Contains(m.ChildId))
                           .Join(DeviceHierarchy, h => h.ParentId, h => h.ParentId, (_, h2) => h2)
                           .Join(Devices, h => h.ChildId, d => d.Id, (_, d) => d)
                           .Distinct();
    }

    public IQueryable<Device> GetDeviceDescendantTree(int? deviceId)
    {
      return DeviceHierarchy.Where(m => m.ParentId == deviceId)
                            .Join(Devices, h => h.ChildId, d => d.Id, (_, d) => d);
    }

    public IQueryable<DeviceMeasurementChainHierarchy> GetDeviceChildrenTree(int deviceId)
    {
      // Selectăm doar dispozitivele și excludem lanțurile de măsurare
#pragma warning disable CS8601 // Possible null reference assignment.
      return GetDeviceTree(deviceId).Select(d => new DeviceMeasurementChainHierarchy
      {
        IsMeasurementChainChild = false, // Marcăm explicit că nu este un copil al unui lanț de măsurare
        DeviceId = d.Id,
        ParentId = d.ParentId,
        ItemNumber = d.ItemNumber,
        CalibrationDate = d.CalibrationDate,
        CalibrationInterval = d.CalibrationInterval,
        Position = 0,
        HasActiveCalibrationOrder = d.CalibrationOrders.Any(co => co.StatusHistory
            .OrderByDescending(sh => sh.LastChangedDate)
            .Select(sh => sh.StatusId)
            .FirstOrDefault() < DeviceCalibrationOrderStatus.Active)
      });
#pragma warning restore CS8601 // Possible null reference assignment.
    }
  }
}
