using AutoMapper;
using AutoMapper.QueryableExtensions;
using LineControl.Models;

using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerCore.Models;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;

namespace LineControllerCore.Service
{
  public class CalibrationOrderService : BaseService<DeviceCalibrationOrder>, IDeviceCalibrationService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CalibrationOrderService(LineContextDb context, IMapper mapper, ILogger<CalibrationOrderService> logger, IHttpContextAccessor httpContextAccessor, IIdentityService identityService)
          : base(context, mapper, logger, identityService)
    {
      this._httpContextAccessor = httpContextAccessor;
    }

    public IQueryable<DeviceCalibrationOrderViewModel> GetSelectViewModels()
    {
      return Context.CalibrationOrders.ProjectTo<DeviceCalibrationOrderViewModel>(Mapper.ConfigurationProvider);
    }

    public DeviceCalibrationOrderViewModel GetDeviceCalibrationById(int id)
    {
      var query = Context.CalibrationOrders.Include(c => c.Device)
                                           .ThenInclude(d => d.CreatedBy)
                                           .Include(c => c.Device)
                                           .ThenInclude(d => d.CreatedBy)
                                           .Include(c => c.Device.Status)   
                                           .Include(c => c.Root)
                                           .ThenInclude(r => r.Action).Where(s => s.Id == id).FirstOrDefault();
      if (query is null)
      {
        return null;
      }
      else
      {
        var root = query.IsRoot;
        if (root) 
        {
          query.Edited = true;
        }

        var calibrationOrder = Mapper.Map<DeviceCalibrationOrderViewModel>(query);
        return calibrationOrder;
      }          
    }

    public DeviceCalibrationOrderViewModel Update(DeviceCalibrationOrderViewModel model)
    {
      if (model == null || model.Id == 0)
      {
        throw new ArgumentException("Model is invalid or ID is not provided.");
      }

      DeviceCalibrationOrder entity = Context.CalibrationOrders.Include(s => s.Device).Include(c => c.Root).FirstOrDefault(s => s.Id == model.Id);

      if (entity is null)
      {
        throw new KeyNotFoundException($"DeviceCalibrationOrder with ID {model.Id} not found.");
      }


      model.CreatedById = (int)entity.Device.CreatedById;
      var rootBackup = entity.Root;
      var rootIdBackup = entity.RootId;
      var existingDeviceId = entity.DeviceId;

      // 🔹 mapăm doar câmpurile CalibrationOrder (fără Root)
      Mapper.Map(model, entity);

      // 🔹 restaurăm DeviceId-ul valid
      entity.DeviceId = existingDeviceId;

      if (entity.IsRoot)
      {
        // actualizezi proprietățile din Root
        if (entity.Root != null)
        {
          entity.Root.Comment = model.Comment;
          entity.Root.AccountingNumber = model.AccountingNumber;
         
        }
      }

      if (entity.Edited)
      {
        // actualizezi proprietățile din Device
        if (entity.Device != null)
        {
          entity.Device.SerialNumber = model.SerialNumber;
          entity.Device.CalibrationLocation = model.CalibrationLocation;
          // etc...
        }
      }

      // 🔹 asigurăm consistența EF
      entity.Device = Context.Devices.FirstOrDefault(d => d.Id == existingDeviceId);
      entity.Root = rootBackup;
      entity.RootId = rootIdBackup;

      Context.Update(entity);
      Context.SaveChanges();

      return Mapper.Map<DeviceCalibrationOrderViewModel>(entity);
    }

    public DeviceCalibrationOrderViewModel AddCalibrationOrder(DeviceCalibrationOrderViewModel model)
    {
      var device = Context.Devices.Include(d => d.Parent).Include(d => d.CalibrationOrders).ThenInclude(co => co.Root).FirstOrDefault(d => d.Id == model.DeviceId);
      bool isRoot = device?.Parent == null;

      Logger.LogInformation($"ParentId: {device.ParentId}");
      Logger.LogInformation($"Parent: {(device.Parent != null ? "Populat" : "Null")}");
      if (device != null)
      {
        model.ItemNumber = device.ItemNumber;
      }

      if (model.DeviceId <= 0)
      {
        throw new InvalidOperationException("ID-ul dispozitivului nu este valid.");
      }
      Logger.LogInformation($"[DEBUG] DeviceId din model: {model.DeviceId}");


      var existingCalibration = Context.CalibrationOrders.Include(c => c.Device) .FirstOrDefault(c => c.Device != null && c.Device.Id == model.DeviceId);

      if (existingCalibration != null)
      {
        Logger.LogWarning($"Calibration order already exists for device with ItemNumber {model.ItemNumber}.");
        throw new InvalidOperationException($"Există deja o calibrare pentru dispozitivul cu ItemNumber {model.ItemNumber}.");
      }

      if (string.IsNullOrEmpty(model.AccountingNumber))
      {
        throw new InvalidOperationException("AccountingNumber este obligatoriu.");
      }

      var calibrationLocation = Context.CalibrationLocations.Where(cl => cl.Id == model.CalibrationLocationId)
                                                           .Select(cl => new
                                                           {
                                                             cl.Id,
                                                             cl.Code,
                                                             cl.CostCenter,
                                                             CompanyName = cl.CompanyLocation.Name,
                                                             IsRequired = !string.IsNullOrEmpty(cl.Code) && !string.IsNullOrEmpty(cl.CostCenter)
                                                           })
                                                           .FirstOrDefault();

      if (calibrationLocation == null)
      {
        throw new InvalidOperationException("Locația de calibrare nu este validă.");
      }

      if (!calibrationLocation.IsRequired)
      {
        throw new InvalidOperationException("Locația de calibrare nu are setate câmpurile Code și CostCenter.");
      }

      DeviceCalibrationOrderRoot? parentRoot = null;
      if (!isRoot && device.Parent != null)
      {
        parentRoot = device.Parent.CalibrationOrders
            .OrderByDescending(o => o.Id)
            .Select(o => o.Root)
            .FirstOrDefault();
      }

      var root = new DeviceCalibrationOrderRoot
      {
        AccountingNumber = isRoot
            ? model.AccountingNumber
            : parentRoot?.AccountingNumber ?? model.AccountingNumber,

        AccountingType = isRoot
            ? model.AccountingType
            : parentRoot?.AccountingType ?? model.AccountingType,

        ActionId = (int)(isRoot
            ? model.ActionId ?? throw new InvalidOperationException("ActionId este null.")
            : parentRoot?.ActionId ?? model.ActionId ?? throw new InvalidOperationException("ActionId este null.")),

        ReceiverId = isRoot
            ? model.ReceiverId
            : parentRoot?.ReceiverId ?? model.ReceiverId,

        Comment = model.Comment,
        NoChannels = (int)model.NoChannels

      };

      var deviceCalibration = new DeviceCalibrationOrder
      {
        DeviceId = model.DeviceId,
        CalibrationDate = model.CalibrationDate ?? DateTime.Now,
        TestLocation = calibrationLocation.Code,
        Root = root,
        IsRoot = isRoot,
        Edited = false,
        SendEmail = model.SendEmail
      };

      Context.CalibrationOrders.Add(deviceCalibration);
      Context.SaveChanges();


      return model;
    }

    public async Task<IEnumerable<DeviceViewModel>> GetItemNumbers(string itemNumber)
    {
      var devicesQuery = Context.Devices.Where(s => s.ItemNumber != null);

      if (!string.IsNullOrEmpty(itemNumber))
      {
        devicesQuery = devicesQuery.Where(s => s.ItemNumber.Contains(itemNumber));
      }

      var devices = await devicesQuery
          .Select(s => new DeviceViewModel
          {
            Id = s.Id,
            ItemNumber = s.ItemNumber,
          })
          .ToListAsync();

      return devices;
    }

    public async Task<IEnumerable<CalibrationLocationViewModel>> GetLocationAsync()
    {
      var result = await Context.CalibrationLocations
        .Include(c => c.CompanyLocation)
        .OrderBy(c => c.CompanyLocation.Name)
        .Select(c => new CalibrationLocationViewModel
        {
          Id = c.Id,
          Name = c.CompanyLocation.Name,
          Code = c.Code,
          CostCenter = c.CostCenter
        })
        .ToListAsync();

      return result;
    }

    public async Task<List<CalibrationOrderActionViewModel>> GetCalibrationAction()
    {
      var result = await Context.CalibrationActions.OrderBy(s => s.Id).Select(s => new CalibrationOrderActionViewModel()
      {
        Id= s.Id,
        Name = s.Name,
      }).ToListAsync().ConfigureAwait(false);

      return result;
    }

    public async Task<List<UserSelectViewModel>> GetUserCalibration()
    {
      var result = await Context.Users.OrderBy(s => s.Id).Select(s => new UserSelectViewModel()
      {
        Id = s.Id,
        FirstName = s.FirstName,
        LastName = s.LastName,
        Department = s.Department,
      }).ToListAsync().ConfigureAwait(false);

      return result;
    }

    public async Task<DeviceViewModel> GetDeviceCreator(int deviceId)
    {
      var createby = await Context.Devices.Where(s => s.Id == deviceId).Include(s => s.CreatedBy).Select(s => new DeviceViewModel()
      {
        Id = s.Id,
        CreatedBy = s.CreatedBy.FirstName + " " + s.CreatedBy.LastName
      }).FirstOrDefaultAsync();
      return createby;
    }

    public async Task<DeviceViewModel> GetDeviceTestLocation(int deviceId)
    {
      var testLocation = await Context.Devices.Where(s => s.Id == deviceId).Include(s => s.StoragePlace).ThenInclude(s => s.CompanyLocation).Select(s => new DeviceViewModel()
      {
        Id = s.Id,
        StoragePlace = s.StoragePlace.CompanyLocation.Country + " " + s.StoragePlace.CompanyLocation.Name + " " + s.StoragePlace.Building
      }).FirstOrDefaultAsync();
      return testLocation;
    }
  }
}
