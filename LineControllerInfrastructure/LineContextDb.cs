using LineControllerCore.Interface;
using LineControllerCore.Model;

using LineControllerInfrastructure.ContextConfiguration;
using LineControllerInfrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Reflection;
using System.Text.Json;

namespace LineControllerInfrastructure
{
  public class LineContextDb : DbContext
  {
    private readonly IServiceProvider _serviceProvider;
    public LineContextDb(DbContextOptions<LineContextDb> options, IServiceProvider serviceProvider) : base(options)
    {
      _serviceProvider = serviceProvider;
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

    public DbSet<Device> Devices { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<StoragePlace> StoragePlaces { get; set; }

    public DbSet<DeviceCalibrationOrder> CalibrationOrders { get; set; }

    public DbSet<DeviceCalibrationOrderStatus> CalibrationStatuses { get; set; }

    public DbSet<CalibrationAction> CalibrationActions { get; set; }

    public DbSet<CalibrationLocation> CalibrationLocations { get; set; }

    public DbSet<DeviceCalibrationOrderRoot> DeviceCalibrationOrderRoots { get; set; }

    public DbSet<Manufacturer> Manufacturers { get; set; }

    public DbSet<DeviceClass> DeviceClass { get; set; }


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
    }

    public override int SaveChanges()
    {
      return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      var auditEntries = OnBeforeSaveChanges();
      var result = await base.SaveChangesAsync(cancellationToken);

      if (auditEntries.Any())
      {
        foreach (var history in auditEntries)
        {
          if (history.Action == "Create" || history.DeviceId == 0)
          {
            if (history.Device != null)
            {
              history.DeviceId = history.Device.Id;
            }
          }
        }
        await DeviceHistories.AddRangeAsync(auditEntries, cancellationToken);
        await base.SaveChangesAsync(cancellationToken); // Salvăm istoricul
      }

      return result;
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


    public IQueryable<Device> GetDeviceTree(int? deviceId)
    {
      return Devices.Where(m => m.Id == deviceId || m.ParentId == deviceId);
    }

    public IQueryable<Device> GetDeviceDescendantTree(int? deviceId)
    {
      return Devices.Where(d => d.Id == deviceId || d.ParentId == deviceId);
    }

    private List<DeviceHistory> OnBeforeSaveChanges()
    {
      var identityService = _serviceProvider.GetService<IIdentityService>();
      ChangeTracker.DetectChanges();
      var historyEntries = new List<DeviceHistory>();
      var userId = identityService.UserId.Value;

      // Căutăm doar entitățile de tip DEVICE care sunt modificate/adăugate/șterse
      var entries = ChangeTracker.Entries<Device>()
                                 .Where(e => e.State == EntityState.Added ||
                                             e.State == EntityState.Modified ||
                                             e.State == EntityState.Deleted);

      foreach (var entry in entries)
      {
        var history = new DeviceHistory
        {
          ModificationDate = DateTime.Now,
          ModificationUserId = userId,
          // Dacă e sters, nu mai are DeviceId valid uneori, dar la Update/Add are
          Device = (Device)entry.Entity,

          DeviceId = entry.State == EntityState.Deleted ? (int)entry.Property("Id").OriginalValue : (int)entry.Property("Id").CurrentValue
        };

        // Serializăm stările pentru a le pune în OldValue/NewValue
        // Folosim un Dictionary pentru a stoca doar proprietățile, nu tot obiectul greoi
        var oldValues = new Dictionary<string, object>();
        var newValues = new Dictionary<string, object>();

        foreach (var property in entry.Properties)
        {
          string propertyName = property.Metadata.Name;

          // Ignorăm colecțiile sau proprietățile care nu ne interesează
          if (property.Metadata.IsPrimaryKey()) continue;

          switch (entry.State)
          {
            case EntityState.Added:
              history.Action = "Create";
              newValues[propertyName] = property.CurrentValue;
              break;

            case EntityState.Deleted:
              history.Action = "Delete";
              oldValues[propertyName] = property.OriginalValue;
              break;

            case EntityState.Modified:
              history.Action = "Update";
              if (property.IsModified)
              {
                oldValues[propertyName] = property.OriginalValue;
                newValues[propertyName] = property.CurrentValue;
              }
              break;
          }
        }

        // Convertim dicționarele în JSON string
        history.OldValue = oldValues.Count > 0 ? JsonSerializer.Serialize(oldValues) : null;
        history.NewValue = newValues.Count > 0 ? JsonSerializer.Serialize(newValues) : null;

        // Pentru CREATE, DeviceId-ul este temporar 0 până după save, 
        // dar logica de mai sus îl va captura la pasul următor dacă e nevoie, 
        // sau putem accepta că la create nu avem id imediat disponibil în logica simplă.
        // Pentru simplitate, la UPDATE/DELETE merge perfect.

        historyEntries.Add(history);
      }

      return historyEntries;
    }
  }
}
