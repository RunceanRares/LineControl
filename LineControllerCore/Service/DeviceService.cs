using AutoMapper;
using AutoMapper.QueryableExtensions;
using LineControl.Models;

using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using LineControllerInfrastructure.Entities.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Text.Json;

namespace LineControllerCore.Service
{
  public class DeviceService : BaseService<Device>, IDeviceService
  {
    //private readonly ILinkService linkService;
    //private readonly IUserRoleService userRoleService;

    public DeviceService(LineContextDb context, IMapper mapper, ILogger<DeviceService> logger, IIdentityService identityService)//, ILinkService linkService, IUserRoleService userRoleService) 
           : base(context, mapper, logger, identityService)
    {
      //this.linkService = linkService;
      //this.userRoleService = userRoleService;
    }

    public IQueryable<DeviceViewModel> GetDevices() 
    {
      return Context.Devices
       .AsNoTracking()
       .Where(d => d.ItemNumber != null)
       .ProjectTo<DeviceViewModel>(Mapper.ConfigurationProvider);
    }

    public DeviceEditViewModel GetDeviceById(int id) 
    {
      var device = Context.Devices.Where(s => s.Id == id).FirstOrDefault();
      
      if(device is not null)
      {
        var mapDevice = Mapper.Map<DeviceEditViewModel>(device);
        return mapDevice;
      }
      else
      {
        return null;
      }

    }

    public async Task<IEnumerable<MeasurementRangeViewModel>> GetMeasurementRangesAsync(int deviceClassId)
    {
      var measurements = Entities.Where(dc => dc.Id == deviceClassId)
                                 .Where(mode => mode.MeasurementMin != null &&
                                                mode.MeasurementMax != null &&
                                                mode.MeasurementUnit != null)
                                 .Select(mode => new
                                 {
                                   mode.Id,
                                   mode.MeasurementMin,
                                   mode.MeasurementMax,
                                   mode.MeasurementUnit,
                                   mode.MaterialNumber,
                                 })
                                 .GroupBy(mode => new
                                 {
                                   mode.MeasurementMin,
                                   mode.MeasurementMax,
                                   mode.MeasurementUnit,
                                 });
      if (deviceClassId != null)
      {
        return await measurements.Select(kvp => new MeasurementRangeViewModel()
        {
          Min = kvp.Key.MeasurementMin.Value,
          Max = kvp.Key.MeasurementMax.Value,
          Unit = kvp.Key.MeasurementUnit,
        }).ToListAsync().ConfigureAwait(false);
      }
      else
      {
        return await measurements.Select(kvp => new MeasurementRangeViewModel()
        {
          Min = kvp.Key.MeasurementMin.Value,
          Max = kvp.Key.MeasurementMax.Value,
          Unit = kvp.Key.MeasurementUnit,
        }).ToListAsync().ConfigureAwait(false);
      }
    }

    public async Task<IEnumerable<DeviceStatusViewModel>> GetStatusesAsync()
    {
      var result = await Context.DeviceStatuses.OrderBy(s => s.Id)
                                .ProjectTo<DeviceStatusViewModel>(Mapper.ConfigurationProvider, new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase))
                                .ToListAsync().ConfigureAwait(false);

      return result;
    }

    public async Task<DeviceEditViewModel> Update(DeviceEditViewModel model)
    {
      DateTime dateTime = DateTime.Now;
      bool? isRoot = await Context.Devices.AsNoTracking()
                                .Where(d => d.Id == model.Id)
                                .Select(d => d.ParentId == null)
                                .FirstOrDefaultAsync();
      bool hasChangedStatus = false;

      var deviceDescendant = await Context.GetDeviceDescendantTree(model.Id).ToListAsync();

      foreach (var device in deviceDescendant)
      {
        //Entities.Attach(device);
        if (device.Id == model.Id)
        {
          if (model.StatusId != device.StatusId)
          {
            hasChangedStatus = true;
          }
          Mapper.Map(model, device);
          device.LastChangedUserId = IdentityService.UserId;
          device.LastChangedDate = dateTime;
        }
        else if (isRoot == true)
        {
          device.StatusId = model.StatusId;
          device.LastChangedUserId = IdentityService.UserId;
          device.LastChangedDate = dateTime;
        }
        if (device.StoragePlaceId == 0)
        {
          device.StoragePlaceId = null;
        }

        device.StoragePlace = null;

        Context.Devices.Update(device);
      }

      await Context.SaveChangesAsync().ConfigureAwait(false);

      var updatedDevice = await Context.Devices.AsNoTracking().Where(d => d.Id == model.Id).FirstOrDefaultAsync().ConfigureAwait(false);

      return Mapper.Map<DeviceEditViewModel>(updatedDevice);
    }

    public async Task<DeviceEditViewModel> AddDevice(DeviceEditViewModel model)
    {
      var existingDevice = await CheckDeviceExist(model).ConfigureAwait(false);
      if(existingDevice != null) 
      {
        Logger.LogWarning("Unable to add device. Id already exist.");
        return null;
      }
      else
      {
        var deviceModel = Mapper.Map<Device>(model);

        deviceModel.Id = 0;

        Context.Devices.Add(deviceModel);
        var rowsAffected = await Context.SaveChangesAsync().ConfigureAwait(false);

        if (rowsAffected == 0)
        {
          Logger.LogWarning("SaveChangesAsync s-a executat, dar a returnat 0 (nimic salvat).");
        }
        else
        {
          Logger.LogInformation($"Succes! Au fost salvate {rowsAffected} randuri. Noul ID: {deviceModel.Id}");
        }

        // Remapează pentru a returna ID-ul nou generat
        return Mapper.Map<DeviceEditViewModel>(deviceModel);
      }
    }     
    
    private async Task<DeviceEditViewModel> CheckDeviceExist(DeviceEditViewModel model)
    {
      if (model.Id == 0)
      {
        return null;
      }

      var device = Context.Devices.FirstOrDefault(d => d.Id == model.Id);
      if (device != null)
      {
        Logger.LogInformation("Unable to add the activity type. Id already exists.");
        return Mapper.Map<DeviceEditViewModel>(device);
      }
      else
      {
        return model;
      }
    }

    public bool CheckUserInDB(int userId)
    {
      return Context.Users.Any(s => s.Id == userId);
    }

    public List<DeviceClassViewModel> GettAllDeviceClass()
    {
      var result = Context.DeviceClass.Select(c => new DeviceClassViewModel
      {
        Id = c.Id,
        ManufacturerName = c.Manufacturer.Name,
        DeviceModelName = c.DeviceModel.Name,
      }).ToList();

      return result;
    }


    public List<InventoryLocationViewModel> GetAllInventotyLocation()
    {
      var result = Context.InventoryLocations.Select(c => new InventoryLocationViewModel
      {
        Id = c.Id,
        Name = c.Name
      }).ToList();

      return result;
    }

    public async Task<DeviceChildViewModel> IntegrateAsync(int parentId, DeviceChildViewModel model)
    {
      try
      {
        var parent = await Context.Devices.SingleOrDefaultAsync(d => d.Id == parentId);

        if (parent == null)
          throw new ArgumentException("Parent device does not exist.");

        var child = await Context.Devices
            .Include(d => d.DeviceClass)
                .ThenInclude(dc => dc.Manufacturer)
            .Include(d => d.DeviceClass)
                .ThenInclude(dc => dc.DeviceModel)
            .Include(d => d.CalibrationOrders)
            .SingleOrDefaultAsync(d => d.ItemNumber == model.ItemNumber);

        if (child == null)
          throw new ArgumentException("Device with the given item number does not exist.");

        //if (child.ParentId == parentId)
        //  throw new ArgumentException("Device is already integrated.");

        // 2. Integrarea propriuzisă
        child.ParentId = parentId;
        child.LastChangedDate = DateTime.Now;
        child.LastChangedUserId = IdentityService.UserId;

        await Context.SaveChangesAsync();

        // 3. Logica pentru calibration order (logica ta reparată)
        var calibrationOrderRoot = await Context.ActiveCalibrationOrders.SingleOrDefaultAsync(c => c.DeviceId == parentId);

        if (calibrationOrderRoot != null)
        {
          var status = new DeviceCalibrationOrderStatusHistory()
          {
            StatusId = DeviceCalibrationOrderStatus.Received,
            LastChangedDate = DateTime.Now,
            LastChangedUserId = IdentityService.UserId
          };

          var calibrationOrder = new DeviceCalibrationOrder
          {
            DeviceId = child.Id,
            SendEmail = true,
            IsRoot = false,
            LastChangedDate = status.LastChangedDate,
            LastChangedUserId = status.LastChangedUserId
          };

          calibrationOrder.StatusHistory.Add(status);
          await Context.CalibrationOrders.AddAsync(calibrationOrder);
          await Context.SaveChangesAsync();
        }

        // 4. Returnăm modelul pentru Kendo TreeList
        return new DeviceChildViewModel
        {
          Id = child.Id,
          ParentId = parentId,
          ItemNumber = child.ItemNumber,
          Manufacturer = child.DeviceClass.Manufacturer.Name,
          DeviceModel = child.DeviceClass.DeviceModel.Name,
          HasActiveCalibrationOrder = child.CalibrationOrders.Any(o => o.IsRoot == false),
        };
      }
      catch (Exception ex)
      {
        Logger.LogError("The following exception occurred while trying to integrate the device");
        return null;
      }
    }

    public async Task<DeviceInformationViewModel> GetDeviceInformationAsync(string itemNumber)
    {
      var model = new DeviceInformationViewModel();

      model.ItemNumber = itemNumber;
      model.Id = await Context.Devices.Where(m => m.ItemNumber == itemNumber)
                                      .Select(m => m.Id)
                                      .SingleOrDefaultAsync().ConfigureAwait(false);
      return model;
    }

    public async Task<DeviceReservationEditViewModel> GetDeviceReservationEditViewModelAsync(string? itemNumber)
    {
      // 1. Pagina deschisă prima dată -> VM gol
      if (string.IsNullOrWhiteSpace(itemNumber))
      {
        return new DeviceReservationEditViewModel();
      }

      // 2. Căutăm device-ul după item number
      var device = await Context.Devices
          .FirstOrDefaultAsync(d => d.ItemNumber == itemNumber);

      // 2.a Device INEXISTENT -> returnăm VM cu ItemNumber și restul gol
      if (device == null)
      {
        return new DeviceReservationEditViewModel
        {
          ItemNumber = itemNumber
        };
      }

      // 3. Căutăm rezervarea activă
      var activeReservation = await Context.DeviceReservations
          .Include(r => r.InventoryLocation)
          .Include(r => r.Issue)
          .FirstOrDefaultAsync(r =>
              r.DeviceId == device.Id &&
              r.StatusId == (int)ReservationStatusEnum.Open
          );

      // 4. Dacă există rezervare activă → ViewMode EDIT
      if (activeReservation != null)
      {
        return new DeviceReservationEditViewModel
        {
          ReservationId = activeReservation.Id,
          DeviceId = device.Id,
          ItemNumber = device.ItemNumber,

          MeasurementMin = activeReservation.MeasurementMin,
          MeasurementMax = activeReservation.MeasurementMax,
          MeasurementUnit = activeReservation.MeasurementUnit,

          StartDate = activeReservation.StartDate,
          InventoryLocationId = activeReservation.InventoryLocationId,
          AccountingNumber = activeReservation.AccountingNumber,

          PeriodId = null,
          Designation = device.Comment,

          // flaguri
          IsUniversal = false,
          HasMultipleMeasurementRanges = false
        };
      }

      // 5. Device EXISTĂ dar NU are rezervare activă → ViewMode CREATE
      return new DeviceReservationEditViewModel
      {
        DeviceId = device.Id,
        ItemNumber = device.ItemNumber,

        // date din device
        MeasurementMin = device.MeasurementMin,
        MeasurementMax = device.MeasurementMax,
        MeasurementUnit = device.MeasurementUnit,

        Designation = device.Comment,

        // pregătit pentru creare rezervare
        InventoryLocationId = device.StoragePlaceId,
        StartDate = DateTime.Today
      };
    }

    public async Task<DeviceHistoryViewModel?> GetDeviceHistoryAsync(string itemNumber)
    {
      if (string.IsNullOrWhiteSpace(itemNumber)) return null;

      var device = await Context.Devices
          .AsNoTracking()
        .Include(d => d.DeviceClass)               
            .ThenInclude(dc => dc.DeviceModel)    
        .Include(d => d.DeviceClass)
            .ThenInclude(dc => dc.Manufacturer)
        .FirstOrDefaultAsync(d => d.ItemNumber == itemNumber);

      if (device == null) return null;

      var model = new DeviceHistoryViewModel
      {
        Id = device.Id,
        ItemNumber = device.ItemNumber,
        DeviceModel = device.DeviceClass?.DeviceModel.Name ?? "-",
        Manufacturer = device.DeviceClass?.Manufacturer.Name ?? "-"
      };

      // 3. Căutăm istoricul asociat acestui DeviceId
      var historyEntities = await Context.DeviceHistories
          .AsNoTracking()
          .Include(h => h.ModificationUser)
          .Where(h => h.DeviceId == device.Id)
          .OrderByDescending(h => h.ModificationDate) 
          .ToListAsync();

      foreach (var h in historyEntities)
      {
        model.HistoryEntries.Add(new DeviceHistoryDetailViewModel
        {
          Date = h.ModificationDate,
          User = h.ModificationUser != null ? $"{h.ModificationUser.FirstName} {h.ModificationUser.LastName}" : "System/Unknown",
          Action = h.Action,
          Details = FormatHistoryDetails(h.Action, h.OldValue, h.NewValue)
        });
      }

      return model;
    }

    // Helper privat pentru a face textul frumos din JSON
    private string FormatHistoryDetails(string action, string? oldJson, string? newJson)
    {
      if (action == "Create") return "Device created.";
      if (action == "Delete") return "Device deleted.";

      if (action == "Update" && !string.IsNullOrEmpty(newJson))
      {
        try
        {
          var newVals = JsonSerializer.Deserialize<Dictionary<string, object>>(newJson);
          var oldVals = !string.IsNullOrEmpty(oldJson)
              ? JsonSerializer.Deserialize<Dictionary<string, object>>(oldJson)
              : new Dictionary<string, object>();

          var changes = new List<string>();

          if (newVals != null)
          {
            foreach (var key in newVals.Keys)
            {
              var oVal = oldVals != null && oldVals.ContainsKey(key) ? oldVals[key]?.ToString() : "null";
              var nVal = newVals[key]?.ToString();
              changes.Add($"{key}: {oVal} -> {nVal}");
            }
          }

          return string.Join(", ", changes);
        }
        catch
        {
          // Dacă eșuează parsarea, returnăm raw data
          return "Data updated.";
        }
      }

      return string.Empty;
    }
  }
}
